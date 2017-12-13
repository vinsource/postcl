using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using HtmlAgilityPack;

namespace Vinclapp.Craigslist
{
    public class CraigslistService
    {
        //Email: TestyFlavor518@yahoo.com - Password: wCjdpMyD02
        #region Const
        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0;" +
                                         "Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; " +
                                         ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30618; " +
                                         "InfoPath.2; OfficeLiveConnector.1.3; OfficeLivePatch.0.0)";
        private const string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        private const string CryptedStepCheckPattern = "<input type=\"hidden\" name=\"cryptedStepCheck\" value=\"([^\\\"]*)\">";
        private const string AcceptLanguage = "Accept-Language: en-US,en;q=0.5";
        private const string AcceptEncoding = "Accept-Encoding: gzip, deflate";
        private const string ContentType = "application/x-www-form-urlencoded";
        private const string LogInUrl = "https://accounts.craigslist.org/login";
        private const string SubmitLogInUrl = "https://accounts.craigslist.org/login";
        #endregion

        //private ICommonManagementForm _commonManagementForm;

        #region Properties

        //private IEmail _emailHelper;
        public string Email { get; set; }
        public string Password { get; set; }
        public int StatusCode { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public CookieCollection CookieCollection { get; set; }
        public string CryptedStepCheck { get; set; }
        #endregion

        #region Constructor
        public CraigslistService()
        {
            CookieContainer = new CookieContainer();
            CookieCollection = new CookieCollection();
            //_emailHelper = new Email();
            //_commonManagementForm = new CommonManagementForm();
        }
        #endregion

        public ConfirmationPayment PostingAdsOnCraigslist(string email, string password, VehicleInfo vehicle, CreditCardInfo creditCard)
        {
            //Step 2: log on
            if (StatusCode == 0)
            {
                WebRequestPost(email, password);
                if (StatusCode != 302)
                {
                    return new ConfirmationPayment { Status = CraigslistPostingStatus.Error, ErrorMessage = "You forgot to input Username/Password in Admin setting? or Your account is invalid." };
                }
            }

            if (string.IsNullOrEmpty(vehicle.CraigslistCityUrl))
            {
                return new ConfirmationPayment { Status = CraigslistPostingStatus.Error, ErrorMessage = "You forgot to set State/City/Location in Admin setting. Let's do that first." };
            }

            var locationPostUrl = GetLocationPostUrl(vehicle.CraigslistCityUrl);

            var locationUrl = GetEncodedLocationUrl(locationPostUrl);
            var typePostingUrl = GetTypePostingUrl(locationUrl);

            var cryptedStepCheck = GetCryptedStepCheckFromUrl(typePostingUrl);

            //Step 3: get category & Crypted code
            var categoryChoosingUrl = GetCategoryChoosingUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(categoryChoosingUrl);

            //Step 4: get sub location & Crypted code
            var subLocationChoosingUrl = GetSubLocationChoosingUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(subLocationChoosingUrl);
            
            //Step 4: go to create posting & get Crypted code
            if (vehicle.CityIndex > 0)
            {
                var createPostingUrl = GetCreatePostingUrl(vehicle.CityIndex, locationUrl, cryptedStepCheck);
                cryptedStepCheck = GetCryptedStepCheckFromUrl(createPostingUrl);
            }

            //Step 5: posting
            var imageEditingUrl = Posting(vehicle, locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(imageEditingUrl);

            //Step 6: upload images
            UploadImages(locationUrl, cryptedStepCheck, vehicle);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(imageEditingUrl);

            //Step 7: preview
            var previewUrl = GetPreviewUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(previewUrl);

            //Step 8: go to billing page & get Crypted code
            var billingUrl = GetBillingUrl(locationUrl, cryptedStepCheck, 1);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(billingUrl);
            GetBillingUrl(locationUrl, cryptedStepCheck, 2);

            if (billingUrl.Contains("mailoop"))
            {
                return new ConfirmationPayment()
                {
                    Status = CraigslistPostingStatus.EmailVerification,
                    ErrorMessage = "This is your first post on this device so you should receive an email shortly, with a link to confirm your ad. Please check Inbox or Spam " + email
                };
            }

            //Step 9: payment
            var paymentUrl = GetPaymentUrl(billingUrl);

            if (String.IsNullOrEmpty(paymentUrl))
            {
                return new ConfirmationPayment()
                {
                    Status = CraigslistPostingStatus.PaymentError,
                    ErrorMessage = "PaymentError: please look at billing URL for more detail " + billingUrl
                };
                
            }

            creditCard.CryptedStepCheck = cryptedStepCheck;
            
            var confirmationPaymentUrl = paymentUrl;
            try
            {
                confirmationPaymentUrl = Purchase(paymentUrl, creditCard);

                if (String.IsNullOrEmpty(confirmationPaymentUrl))
                {
                    return new ConfirmationPayment()
                    {
                        Status = CraigslistPostingStatus.PaymentError,
                        ErrorMessage = "PaymentError: Cannot purchase " + confirmationPaymentUrl
                    };

                }

                return GetConfirmationPaymentInfo(confirmationPaymentUrl);
            }
            catch (Exception)
            {
                return new ConfirmationPayment()
                {
                    Status = CraigslistPostingStatus.PaymentError,
                    ErrorMessage = "PaymentError: Cannot purchase (Exception) " + confirmationPaymentUrl
                };
            }
            
        }
    
        public PostingPreview GoToPostingPreviewPage(string email, string password, VehicleInfo vehicle)
        {           
            //Step 2: log on
            WebRequestPost(email, password);

            var locationPostUrl = GetLocationPostUrl(vehicle.CraigslistCityUrl);

            var locationUrl = GetEncodedLocationUrl(locationPostUrl);
            var typePostingUrl = GetTypePostingUrl(locationUrl);

            var cryptedStepCheck = GetCryptedStepCheckFromUrl(typePostingUrl);

            //Step 3: get category & Crypted code
            var categoryChoosingUrl = GetCategoryChoosingUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(categoryChoosingUrl);

            //Step 4: get sub location & Crypted code
            var subLocationChoosingUrl = GetSubLocationChoosingUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(subLocationChoosingUrl);

            //Step 4: go to create posting & get Crypted code
            if (vehicle.CityIndex > 0)
            {
                
                var createPostingUrl = GetCreatePostingUrl(vehicle.CityIndex, locationUrl, cryptedStepCheck);
                cryptedStepCheck = GetCryptedStepCheckFromUrl(createPostingUrl);
            }

            //Step 5: posting
            var imageEditingUrl = Posting(vehicle, locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(imageEditingUrl);

            //Step 6: upload images
            UploadImages(locationUrl, cryptedStepCheck, vehicle);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(imageEditingUrl);

            //Step 7: preview
            var previewUrl = GetPreviewUrl(locationUrl, cryptedStepCheck);
            var post = PreviewPosting(previewUrl, vehicle);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(previewUrl);

            //Step 8: go to billing page & get Crypted code
            var billingUrl = GetBillingUrl(locationUrl, cryptedStepCheck, 1);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(billingUrl);
            GetBillingUrl(locationUrl, cryptedStepCheck, 2);

            //Step 9: payment
            var paymentUrl = GetPaymentUrl(billingUrl);

            return new PostingPreview()
            {
                Post = post,
                CryptedStepCheck = cryptedStepCheck,
                LocationUrl = paymentUrl
            };
        }

        public ConfirmationPayment GoToPurchasingPage(string email, string password, CreditCardInfo creditCard)
        {
            WebRequestPost(email, password);
            var confirmationPaymentUrl = Purchase(creditCard.LocationUrl, creditCard);

            return GetConfirmationPaymentInfo(confirmationPaymentUrl);
        }

        #region WebRequestGet
        public void WebRequestGet()
        {
            var request = (HttpWebRequest)WebRequest.Create(LogInUrl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = "accounts.craigslist.org";
            request.KeepAlive = true;
            request.Headers.Add(AcceptLanguage);
            request.Headers.Add(AcceptEncoding);
            request.UserAgent = UserAgent;
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);
            //Get the response from the server and save the cookies from the first request..
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                CookieCollection = response.Cookies;
            }
        }
        #endregion

        #region WebRequestPost
        public void WebRequestPost(string email, string password)
        {
            var postData = String.Format("step={0}&rt={1}&rp={2}&inputEmailHandle={3}&inputPassword={4}", "confirmation", string.Empty, string.Empty, HttpUtility.UrlEncode(email), HttpUtility.UrlEncode(password));
            

            // Setup the http request.
            var request = (HttpWebRequest)WebRequest.Create(SubmitLogInUrl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = "accounts.craigslist.org";
            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.ContentLength = postData.Length;
            request.Headers.Add("Accept-Language: en-US,en;q=0.5");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.ContentType = ContentType;
            request.Accept = Accept;
            request.Referer = "https://accounts.craigslist.org/";
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);

            //var result = string.Empty;
            try
            {
                // Post to the login form.
                var streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(postData);
                streamWriter.Close();

                // Get the response.
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // Have some cookies.
                    CookieCollection = response.Cookies;
                    StatusCode = (int)response.StatusCode;
                    // Read the response
                    //var streamReader = new StreamReader(response.GetResponseStream());
                    //result = streamReader.ReadToEnd();
                    //streamReader.Close();
                }
            }
            catch (Exception)
            {
                //result = string.Empty;
            }
        }
        #endregion

        #region GET
        public string GetEncodedLocationUrl(string locationPostUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(locationPostUrl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = "post.craigslist.org";
            request.KeepAlive = true;
            request.Headers.Add(AcceptLanguage);
            request.Headers.Add(AcceptEncoding);
            request.UserAgent = UserAgent;
            request.Accept = Accept;
            request.AllowAutoRedirect = false;
            request.Referer = locationPostUrl;
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                return String.Format("https://post.craigslist.org{0}", response.Headers["Location"]);
            }
        }

        public string GetTypePostingUrl(string locationUrl)
        {
            return ExecuteGet(locationUrl);
        }

        public string GetPaymentUrl(string billingUrl)
        {
            return ExecuteGet(billingUrl);
        }
        #endregion

        #region POST
        public string GetCategoryChoosingUrl(string locationUrl, string cryptedStepCheck)
        {
            var postData = String.Format("id=fsd&cryptedStepCheck={0}&go=Continue", cryptedStepCheck);
            return ExecutePost(locationUrl, postData);
        }

        public string GetSubLocationChoosingUrl(string location)
        {
            var locationPostUrl = GetLocationPostUrl(location);
            var locationUrl = GetEncodedLocationUrl(locationPostUrl);
            var typePostingUrl = GetTypePostingUrl(locationUrl);

            var cryptedStepCheck = GetCryptedStepCheckFromUrl(typePostingUrl);
            var categoryChoosingUrl = GetCategoryChoosingUrl(locationUrl, cryptedStepCheck);
            cryptedStepCheck = GetCryptedStepCheckFromUrl(categoryChoosingUrl);

            return GetSubLocationChoosingUrl(locationUrl, cryptedStepCheck);
        }

        public string GetSubLocationChoosingUrl(string locationUrl, string cryptedStepCheck)
        {
            var postData = String.Format("id=146&cryptedStepCheck={0}&go=Continue", cryptedStepCheck);
            return ExecutePost(locationUrl, postData);
        }

        public string GetCreatePostingUrl(int subLocationId, string locationUrl, string cryptedStepCheck)
        {
            var postData = String.Format("n={0}&cryptedStepCheck={1}&go=Continue", subLocationId, cryptedStepCheck);
            return ExecutePost(locationUrl, postData);
        }

        private string ReplaceEmails(string content, string textToReplace)
        {
            var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(content);

            content = emailMatches.Cast<Match>().Aggregate(content, (current, emailMatch) => current.Replace(emailMatch.Value, textToReplace));
            return content;
        }

        private string GeneratePostingBody(VehicleInfo vehicle)
        {
            var postingBody = EmailTemplateReader.GetCraigslistAdsBodyContent(vehicle.DealerId);
            postingBody = postingBody.Replace(EmailTemplateReader.DealerName, vehicle.DealershipName);
            postingBody = postingBody.Replace(EmailTemplateReader.Address, string.Format("{0} {1},{2} {3}", vehicle.StreetAddress, vehicle.City, vehicle.State, vehicle.ZipCode));
            postingBody = postingBody.Replace(EmailTemplateReader.Phone, vehicle.PhoneNumber);
            postingBody = postingBody.Replace(EmailTemplateReader.DealerWebsite, vehicle.DealerWebUrl);
            postingBody = postingBody.Replace(EmailTemplateReader.Year, vehicle.ModelYear.ToString());
            postingBody = postingBody.Replace(EmailTemplateReader.Make, vehicle.Make);
            postingBody = postingBody.Replace(EmailTemplateReader.Model, vehicle.Model);
            postingBody = postingBody.Replace(EmailTemplateReader.Trim, vehicle.Trim);
            postingBody = postingBody.Replace(EmailTemplateReader.ExteriorColor, vehicle.ExteriorColor);
            postingBody = postingBody.Replace(EmailTemplateReader.Transmission, vehicle.Tranmission);
            postingBody = postingBody.Replace(EmailTemplateReader.Stock, vehicle.StockNumber);
            postingBody = postingBody.Replace(EmailTemplateReader.Vin, vehicle.Vin);
            postingBody = postingBody.Replace(EmailTemplateReader.Option, (!String.IsNullOrEmpty(vehicle.Options)) ? "<br/><b>Options</b><br/>" + vehicle.Options : string.Empty);
            postingBody = postingBody.Replace(EmailTemplateReader.EndingSentence, vehicle.EndingSentence);

            if (!String.IsNullOrEmpty(vehicle.Description))
                postingBody = postingBody.Replace(EmailTemplateReader.Description, "<br/><b>Description</b><br/>" + vehicle.Description);
            else
                postingBody = postingBody.Replace(EmailTemplateReader.Description, string.Empty);

            return postingBody;
        }

        public string Posting(VehicleInfo vehicle, string locationUrl, string cryptedStepCheck)
        {
            var postData = String.Format("language=5&condition=40&id2={0}&" +
                                         "browserinfo={1}&" +
                                         "contact_method={2}&" +
                                         "contact_phone_ok=1&" +
                                         "contact_phone={3}&" +
                                         "contact_name={4}&" +
                                         "FromEMail={5}&" +
                                         "PostingTitle={6}&" +
                                         "Ask={7}&" +
                                         "GeographicArea={8}&" +
                                         "postal={9}&" +
                                         "PostingBody={10}&" +
                                         "auto_year={11}&" +
                                         "auto_make_model={12}&" +
                                         "auto_miles={13}&" +
                                         "auto_vin={14}&" +
                                         "auto_fuel_type={15}&" +
                                         "auto_transmission={16}&" +
                                         "see_my_other={17}&" +
                                         "auto_title_status={18}&" +
                                         "Privacy=C&cryptedStepCheck={19}&condition={20}&sale_condition=excellent&oc=1&go=Continue",
                                         "1903x1045X1903x602X1920x1080",
                                         "%257B%250A%2509%2522plugins%2522%253A%2520%2522Plugin%25200%253A%2520Google%2520Update%253B%2520Google%2520Update%253B%2520npGoogleUpdate3.dll%253B%2520%2528%253B%2520application%2Fx-vnd.google.update3webcontrol.3%253B%2520%2529%2520%2528%253B%2520application%2Fx-vnd.google.oneclickctrl.9%253B%2520%2529.%2520Plugin%25201%253A%2520Silverlight%2520Plug-In%253B%25204.0.50826.0%253B%2520npctrl.dll%253B%2520%2528npctrl%253B%2520application%2Fx-silverlight%253B%2520scr%2529%2520%2528%253B%2520application%2Fx-silverlight-2%253B%2520%2529.%2520%2522%252C%250A%2509%2522timezone%2522%253A%2520480%252C%250A%2509%2522video%2522%253A%2520%25221920x1080x16%2522%252C%250A%2509%2522supercookies%2522%253A%2520%2522DOM%2520localStorage%253A%2520Yes%252C%2520DOM%2520sessionStorage%253A%2520Yes%252C%2520IE%2520userData%253A%2520No%2522%250A%257D",
                                         1,
                                         vehicle.PhoneNumber,
                                         vehicle.ContactName,
                                         vehicle.LeadEmail,
                                         String.Format("{0} {1} {2} {3}", vehicle.ModelYear, vehicle.Make, vehicle.Model, vehicle.Trim),
                                         vehicle.SalePrice,
                                         HttpUtility.UrlEncode(vehicle.CityOveride),
                                         vehicle.ZipCode,
                                         HttpUtility.UrlEncode(GeneratePostingBody(vehicle)),
                                         vehicle.ModelYear,
                                         String.Format("{0} {1}", vehicle.Make, vehicle.Model),
                                         vehicle.Mileage,
                                         vehicle.Vin,
                                         GetFuelType(vehicle),
                                         GetTransmission(vehicle),
                                         1,
                                         1,
                                         cryptedStepCheck,
                                         GetCondition(vehicle));

            return ExecutePost(locationUrl, postData);
        }

        private int GetCondition(VehicleInfo vehicle)
        {
            return vehicle.Condition.ToLower().Equals("new") ? 10 : 40;
        }

        private int GetFuelType(VehicleInfo vehicle)
        {
            var fuel = vehicle.Fuel.ToLower();
            if (fuel.Contains("gas")) return 1;
            if (fuel.Contains("diesel")) return 2;
            if (fuel.Contains("hybrid")) return 3;
            if (fuel.Contains("electric")) return 4;

            return 6;
        }

        private int GetTransmission(VehicleInfo vehicle)
        {
            var tm = vehicle.Tranmission.ToLower();
            if (tm.Contains("manual")) return 1;
            if (tm.Contains("automatic")) return 2;

            return 3;
        }

        public string UploadImages(string locationUrl, string cryptedStepCheck, VehicleInfo vehicle)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            var postData = new Dictionary<string, string>(); ;
            postData.Add("cryptedStepCheck", cryptedStepCheck);
            postData.Add("a", "add");
            postData.Add("go", "add image");

            var request = (HttpWebRequest)WebRequest.Create(locationUrl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = "post.craigslist.org";
            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.Headers.Add("Accept-Language: en-US,en;q=0.5");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Accept = Accept;
            request.Referer = locationUrl + "?s=editimage";
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);
            request.Timeout = 1000000;

            try
            {
                string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" }, StringSplitOptions.RemoveEmptyEntries);
                var tempDir = GetTemporaryDirectory(vehicle.Vin);
                using (WebClient client = new WebClient())
                {
                    foreach (var item in carImage)
                    {
                        client.DownloadFile(new Uri(item), tempDir + "/" + Path.GetFileName(item));
                    }                    
                }                

                using (Stream requestStream = request.GetRequestStream())
                {
                    postData.WriteMultipartFormData(requestStream, boundary);

                    var dirInfo = new DirectoryInfo(tempDir);
                    if (dirInfo.Exists)
                    {
                        var limit = 1;
                        foreach (FileInfo fileToUpload in dirInfo.GetFiles().OrderBy(f => f.CreationTime))
                        {
                            if (fileToUpload != null)
                            {
                                fileToUpload.WriteMultipartFormData(requestStream, boundary, "image/jpeg", "file");
                            }

                            if (limit > 24) break; // Only allow to upload 24 images
                            limit++;
                        }
                    }
                    byte[] endBytes = System.Text.Encoding.UTF8.GetBytes("--" + boundary + "--");
                    requestStream.Write(endBytes, 0, endBytes.Length);
                    requestStream.Close();
                }
            }
            catch (Exception)
            {
                //Error with images
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    return response.Headers["Location"];
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetTemporaryDirectory(string vin)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), vin);//Path.GetRandomFileName()
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        
        public string GetPreviewUrl(string locationUrl, string cryptedStepCheck)
        {
            var postData = String.Format("a=fin&cryptedStepCheck={0}&go=Done+with+Images", cryptedStepCheck);
            return ExecutePost(locationUrl, postData);
        }

        public string GetBillingUrl(string locationUrl, string cryptedStepCheck, int step = 1)
        {
            var postData = step == 1 ? String.Format("continue=y&cryptedStepCheck={0}&go=Continue", cryptedStepCheck) : String.Format("an=ccard&cryptedStepCheck={0}&go=Continue", cryptedStepCheck);
            return ExecutePost(locationUrl, postData);
        }

        public string Purchase(string paymentUrl, CreditCardInfo creditCard)
        {
            var postData = String.Format("cardNumber={0}&" +
                                         "cvNumber={1}&" +
                                         "expMonth={2}&" +
                                         "expYear={3}&" +
                                         "cardFirstName={4}&" +
                                         "cardLastName={5}&" +
                                         "cardAddress={6}&" +
                                         "cardCity={7}&" +
                                         "cardState={8}&" +
                                         "cardPostal={9}&" +
                                         "cardCountry={10}&" +
                                         "contactName={11}&" +
                                         "contactPhone={12}&" +
                                         "contactEmail={13}&" +
                                         "cryptedStepCheck={14}&finishForm=Purchase",
                                         creditCard.CardNumber, creditCard.VerificationNumber, creditCard.ExpirationMonth, creditCard.ExpirationYear,
                                         HttpUtility.UrlEncode(creditCard.FirstName), HttpUtility.UrlEncode(creditCard.LastName), HttpUtility.UrlEncode(creditCard.Address),
                                         HttpUtility.UrlEncode(creditCard.City), creditCard.State, creditCard.Postal, "US", HttpUtility.UrlEncode(creditCard.ContactName),
                                         creditCard.ContactPhone, HttpUtility.UrlEncode(creditCard.ContactEmail), string.Empty);

            return ExecutePost(paymentUrl, postData, "secure.craigslist.org");
        }
        #endregion

        #region Download HTML Content
        public List<SubLocationChoosing> GetSubLocationList(string subLocationChoosingUrl)
        {
            var list = new List<SubLocationChoosing>();
            var xmlDocument = WebHandler.DownloadDocument(WebHandler.DownloadContent(subLocationChoosingUrl, CookieContainer, CookieCollection));
            var locationNodes = xmlDocument.SelectNodes("//form/blockquote/label");
            if (locationNodes != null)
            {
                foreach (XmlNode node in locationNodes)
                {
                    var location = new SubLocationChoosing();
                    location.Value = Convert.ToInt32(WebHandler.GetString(node, "./input/@value", null, null, true));
                    location.Name = node.InnerText.Trim();

                    list.Add(location);
                }
            }

            return list;
        }

      

        public AdsPosting PreviewPosting(string previewUrl,VehicleInfo vehicle)
        {
            var post = new AdsPosting()
            {
                //Location = dealer.CraigslistSetting.City,
                //SubLocation = dealer.CraigslistSetting.Location,
                Type = "for sale / wanted",
                Category = "cars & trucks - by dealer",
                //SpecificLocation = dealer.CraigslistSetting.SpecificLocation,
                Title = String.Format("{0} {1} {2} {3}", vehicle.ModelYear, vehicle.Make, vehicle.Model, vehicle.Trim),
                SalePrice = vehicle.SalePrice,
                Vin = vehicle.Vin,
                Year =Convert.ToInt32(vehicle.ModelYear),
                Make = vehicle.Make,
                Model = vehicle.Model,
                Odometer =Convert.ToInt64(vehicle.Mileage),
                Transmission = String.Format("{0} Transmission", vehicle.Tranmission),
                Body = GeneratePostingBody(vehicle),
                Note = "do NOT contact me with unsolicited"
            };

            var xmlDocument = WebHandler.DownloadDocument(WebHandler.DownloadContent(previewUrl, CookieContainer, CookieCollection));
            var imageNodes = xmlDocument.SelectNodes("//div[@id='thumbs']/a");
            if (imageNodes != null)
            {
                var images = new List<string>();
                foreach (XmlNode node in imageNodes)
                {
                    images.Add(node.Attributes["href"].Value);
                }
                post.Images = images;
            }

            return post;
        }

        public ConfirmationPayment GetConfirmationPaymentInfo(string confirmationPaymentUrl)
        {
            var content = WebHandler.DownloadContent(confirmationPaymentUrl, CookieContainer, CookieCollection);
            //var xmlDocument = WebHandler.DownloadDocument(content);
            try
            {
                var regex = new Regex("Payment ID(.*)<br>");
                var paymentId = Convert.ToInt64(regex.Match(content).Value.Replace("<br>", "").Replace("Payment ID:", ""));
                regex = new Regex("PostingID(.*)<i>");
                //content = xmlDocument.SelectSingleNode("//table[@id='postingInvoice']/tr[3]/td[2]").InnerXml;
                var postingId = Convert.ToInt64(regex.Match(content).Value.Replace("PostingID", "").Replace(":", "").Replace("<i>", ""));
                if (paymentId == 0 || postingId == 0) 
                    return new ConfirmationPayment() { Status = CraigslistPostingStatus.Error, ErrorMessage = confirmationPaymentUrl };

                return new ConfirmationPayment() { PaymentId = paymentId, PostingId = postingId, Status = CraigslistPostingStatus.Success, ErrorMessage = confirmationPaymentUrl };
            }
            catch (Exception ex)
            {
                return new ConfirmationPayment()
                {
                    Status = CraigslistPostingStatus.Error, ErrorMessage = confirmationPaymentUrl
                };
            }
        }

        public string GetLocationPostUrl(string locationUrl)
        {
            var htmlDoc = new HtmlDocument();

            var content = WebHandler.DownloadContent(locationUrl);

            htmlDoc.LoadHtml(content);

            return htmlDoc.DocumentNode.SelectSingleNode("//ul[@id='postlks']//li//a").Attributes["href"].Value;
        }
        #endregion

        #region Private Methods
        private string ExecutePost(string url, string postDate, string host = "post.craigslist.org")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = host;
            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.ContentLength = postDate.Length;
            request.Headers.Add("Accept-Language: en-US,en;q=0.5");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.ContentType = ContentType;
            request.Accept = Accept;
            request.Referer = url;
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);

            try
            {
                var streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(postDate);
                streamWriter.Close();

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //CookieCollection = response.Cookies;

                    //var streamReader = new StreamReader(response.GetResponseStream());
                    //var result = streamReader.ReadToEnd();
                    //streamReader.Close();
                    var returnUrl = response.Headers["Location"];
                    return !string.IsNullOrEmpty(returnUrl) && !returnUrl.Contains("https") ? "https://post.craigslist.org" + returnUrl : returnUrl;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return string.Empty;
            }
        }

        private string ExecuteGet(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Host = "post.craigslist.org";
            request.KeepAlive = true;
            request.Headers.Add(AcceptLanguage);
            request.Headers.Add(AcceptEncoding);
            request.UserAgent = UserAgent;
            request.Accept = Accept;
            request.AllowAutoRedirect = false;
            request.Referer = url;
            request.CookieContainer = CookieContainer;
            request.CookieContainer.Add(CookieCollection);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                //CookieCollection = response.Cookies;

                //var streamReader = new StreamReader(response.GetResponseStream());
                //var result = streamReader.ReadToEnd();
                //streamReader.Close();
                var returnUrl = response.Headers["Location"];
                return !string.IsNullOrEmpty(returnUrl) && !returnUrl.Contains("https") ? "https://post.craigslist.org" + returnUrl : returnUrl;
            }
        }

        private string GetCryptedStepCheckFromUrl(string pageUrl)
        {
            var content = WebHandler.DownloadContent(pageUrl, CookieContainer, CookieCollection);
            var cryptedStepCheck = new Regex(CryptedStepCheckPattern);
            return cryptedStepCheck.Match(content).Groups[1].Value;
        }

        private string GetCryptedStepCheckFromContent(string content)
        {
            var cryptedStepCheck = new Regex(CryptedStepCheckPattern);
            return cryptedStepCheck.Match(content).Groups[1].Value;
        }

        private string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        #endregion
    }

    public class ConfirmationPayment
    {
        public long PaymentId { get; set; }
        public long PostingId { get; set; }
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
    }

    public static class CraigslistPostingStatus
    {
        public static int EmailVerification = 1;
        public static int PaymentError = 4;
        public static int Success = 2;
        public static int Error = 3;

        
    }

    public class CreditCardInfo
    {
        public string CardNumber { get; set; }
        public string VerificationNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string CryptedStepCheck { get; set; }
        public string LocationUrl { get; set; }
        public int ListingId { get; set; }
        
    }

    public class PostingPreview
    {
        public AdsPosting Post { get; set; }
        public string CryptedStepCheck { get; set; }
        public string LocationUrl { get; set; }
    }

    public class AdsPosting
    {
        public string Location { get; set; } //los angeles
        public string SubLocation { get; set; } //long beach / 562
        public string Type { get; set; } //for sale / wanted
        public string Category { get; set; } //cars & trucks - by dealer
        public string SpecificLocation { get; set; }
        public string Title { get; set; }
        public decimal SalePrice { get; set; }
        public string Vin { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public long Odometer { get; set; }
        public string Transmission { get; set; }
        public string Body { get; set; }
        public string Note { get; set; } //do NOT contact me with unsolicited services or offers
        public List<string> Images { get; set; }
    }

    public class StateChoosing
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public List<LocationChoosing> Locations { get; set; }
    }

    public class LocationChoosing
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public List<SubLocationChoosing> SubLocations { get; set; }
    }

    public class SubLocationChoosing
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public static class DictionaryExtensions
    {
        /// <summary>
        /// Template for a multipart/form-data item.
        /// </summary>
        public const string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";

        /// <summary>
        /// Writes a dictionary to a stream as a multipart/form-data set.
        /// </summary>
        /// <param name="dictionary">The dictionary of form values to write to the stream.</param>
        /// <param name="stream">The stream to which the form data should be written.</param>
        /// <param name="mimeBoundary">The MIME multipart form boundary string.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="stream" /> or <paramref name="mimeBoundary" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="mimeBoundary" /> is empty.
        /// </exception>
        /// <remarks>
        /// If <paramref name="dictionary" /> is <see langword="null" /> or empty,
        /// nothing wil be written to the stream.
        /// </remarks>
        public static void WriteMultipartFormData(this Dictionary<string, string> dictionary, Stream stream, string mimeBoundary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                return;
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (mimeBoundary == null)
            {
                throw new ArgumentNullException("mimeBoundary");
            }
            if (mimeBoundary.Length == 0)
            {
                throw new ArgumentException("MIME boundary may not be empty.", "mimeBoundary");
            }
            foreach (string key in dictionary.Keys)
            {
                string item = String.Format(FormDataTemplate, mimeBoundary, key, dictionary[key]);
                byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
                stream.Write(itemBytes, 0, itemBytes.Length);
            }
        }
    }

    public static class FileInfoExtensions
    {
        /// <summary>
        /// Template for a file item in multipart/form-data format.
        /// </summary>
        public const string HeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n";

        /// <summary>
        /// Writes a file to a stream in multipart/form-data format.
        /// </summary>
        /// <param name="file">The file that should be written.</param>
        /// <param name="stream">The stream to which the file should be written.</param>
        /// <param name="mimeBoundary">The MIME multipart form boundary string.</param>
        /// <param name="mimeType">The MIME type of the file.</param>
        /// <param name="formKey">The name of the form parameter corresponding to the file upload.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if any parameter is <see langword="null" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="mimeBoundary" />, <paramref name="mimeType" />,
        /// or <paramref name="formKey" /> is empty.
        /// </exception>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Thrown if <paramref name="file" /> does not exist.
        /// </exception>
        public static void WriteMultipartFormData(this FileInfo file, Stream stream, string mimeBoundary, string mimeType, string formKey)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }
            if (!file.Exists)
            {
                throw new FileNotFoundException("Unable to find file to write to stream.", file.FullName);
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (mimeBoundary == null)
            {
                throw new ArgumentNullException("mimeBoundary");
            }
            if (mimeBoundary.Length == 0)
            {
                throw new ArgumentException("MIME boundary may not be empty.", "mimeBoundary");
            }
            if (mimeType == null)
            {
                throw new ArgumentNullException("mimeType");
            }
            if (mimeType.Length == 0)
            {
                throw new ArgumentException("MIME type may not be empty.", "mimeType");
            }
            if (formKey == null)
            {
                throw new ArgumentNullException("formKey");
            }
            if (formKey.Length == 0)
            {
                throw new ArgumentException("Form key may not be empty.", "formKey");
            }
            string header = String.Format(HeaderTemplate, mimeBoundary, formKey, file.Name, mimeType);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            stream.Write(headerbytes, 0, headerbytes.Length);
            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }
            byte[] newlineBytes = Encoding.UTF8.GetBytes("\r\n");
            stream.Write(newlineBytes, 0, newlineBytes.Length);
        }
    }
}
