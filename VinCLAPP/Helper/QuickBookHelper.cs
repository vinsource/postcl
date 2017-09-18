using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data.Qbo;
using Intuit.Ipp.Security;
using Intuit.Ipp.Services;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class QuickBookHelper
    {
        public static void AddCustomer(PostClCustomer customer)
        {
            //var oauthValidator = new OAuthRequestValidator(
            //    ConfigurationManager.AppSettings["accessToken"].ToString(CultureInfo.InvariantCulture),
            //    ConfigurationManager.AppSettings["accessTokenSecret"].ToString(CultureInfo.InvariantCulture),
            //    ConfigurationManager.AppSettings["consumerKey"].ToString(CultureInfo.InvariantCulture),
            //    ConfigurationManager.AppSettings["consumerKeySecret"].ToString(CultureInfo.InvariantCulture));

            //var context = new ServiceContext(
            //    oauthValidator,
            //    ConfigurationManager.AppSettings["realmId"].ToString(CultureInfo.InvariantCulture),
            //    IntuitServicesType.QBO);

            //var dataServices = new DataServices(context);

            //var newCustomer = new Customer
            //    {
            //        Active = true,
            //        GivenName = customer.FirstName,
            //        FamilyName = customer.LastName,
            //        Name = customer.QuickBookAccountName,
            //        Email = new EmailAddress[1],
            //        Phone = new TelephoneNumber[1],
            //        WebSite = new WebSiteAddress[1],
            //        Address = new PhysicalAddress[1],
            //        DBAName = customer.DealerName,
            //        OpenBalanceDate = DateTime.Now,
            //        SalesTermName = "Due On Reciept",
            //        SalesTermId = new IdType
            //            {
            //                Value = "1"
            //            },
            //        Notes = new Note[1]
            //    };

            //newCustomer.Notes[0] = new Note
            //    {
            //        Content = "New account is just set up without credit card infomation",
            //    };

            //newCustomer.Email[0] = new EmailAddress { Address = customer.CustomerEmail };

            //newCustomer.Phone[0] = new TelephoneNumber
            //    {
            //        FreeFormNumber = customer.CustomerPhone,
            //        DateLastVerified = DateTime.Now
            //    };

    
            //newCustomer.Address[0] = new PhysicalAddress
            //    {
            //        Country = "USA",
            //        City = customer.DealerCity,
            //        CountryCode = "01",
            //        Line1 = customer.DealerStreetAddress,
            //        PostalCode = customer.DealerZipCode,
            //        Tag = new[] { "Billing" }
            //    };

            //var quickbookCustomer = dataServices.Add(newCustomer);

          

            //customer.QuickBookAccountId = quickbookCustomer.Id.Value;

            //if (!String.IsNullOrEmpty(customer.QuickBookAccountId))
            //{
            //    DataHelper.AddNewDealer(customer);
            //}
            DataHelper.AddNewDealer(customer);

        }

        public static bool CreateSalesReceiptAndMakePayment(BillingCustomer customer)
        {
            try
            {
                var oauthValidator = new OAuthRequestValidator(
              ConfigurationManager.AppSettings["accessToken"].ToString(CultureInfo.InvariantCulture),
              ConfigurationManager.AppSettings["accessTokenSecret"].ToString(CultureInfo.InvariantCulture),
              ConfigurationManager.AppSettings["consumerKey"].ToString(CultureInfo.InvariantCulture),
              ConfigurationManager.AppSettings["consumerKeySecret"].ToString(CultureInfo.InvariantCulture));

                var context = new ServiceContext(
                    oauthValidator,
                    ConfigurationManager.AppSettings["realmId"].ToString(CultureInfo.InvariantCulture),
                    IntuitServicesType.QBO);

                var dataServices = new DataServices(context);

                var findCustomer = FindCustomer(customer.QuickBookAccountName);

                var findProduct = FindProduct(customer.SelectedPackage.QuickBookPackageName);

                Item findProductDataFeed=null;

                findCustomer.Notes[0] = new Note()
                {
                    Content = "Card Number = " + customer.CardNumber + ", Expired Month = " + customer.ExpirationMonth + ", Expried Year = " + customer.ExpirationYear +
                     ", Security Code = " + customer.SecurityCode + ", Name = " + customer.NameOnCard + ", Street = " + customer.BillingStreetAddress + ", ZipCode = " + customer.BillingZipCode
                };

                dataServices.Update(findCustomer);


                var salere = new Intuit.Ipp.Data.Qbo.SalesReceipt
                {
                    Id = new IdType
                    {
                        Value = Guid.NewGuid().ToString(),
                        idDomain = idDomainEnum.QBO,
                    },


                    Synchronized = false,
                    SynchronizedSpecified = false,
                    MetaData = new ModificationMetaData
                    {
                        CreateTime = DateTime.Now,
                        CreateTimeSpecified = true,
                        CreatedBy = "POSTCLAdmin",
                        LastUpdatedTime = DateTime.Now,
                        LastUpdatedTimeSpecified = true,
                    },
                    Header = new SalesReceiptHeader()
                    {
                        SubTotalAmt = (decimal)findProduct.UnitPrice.Amount,
                        TaxAmt = (decimal)0,
                        TotalAmt = (decimal)findProduct.UnitPrice.Amount,
                        ToBeEmailed = true,
                        ToBePrinted = true,
                        ToBePrintedSpecified = true,
                        TaxRate = 0,
                        TxnDate = DateTime.Now,
                        Currency = currencyCode.USD,
                        CustomerId = new IdType()
                        {
                            Value = findCustomer.Id.Value,
                            idDomain = findCustomer.Id.idDomain
                        },
                        CustomerName = findCustomer.Name,
                        Msg =
                            "Thank you for using Vincontrol LLC.VinControl . VinCLapp . VinGenie. VinPage.VinSocial . VinCapture . VinSell . VinGreet .",


                    },
                    Line = new SalesReceiptLine[1],
                };

                if (customer.SelectedPackage.Period.Equals("6 months") ||
                    customer.SelectedPackage.Period.Equals("1 year") ||  (customer.SelectedPackage.Period.Equals("1 month") && customer.OneTimeSetUpFeed==false))
                {
                    
                    salere.Line[0] = new SalesReceiptLine
                        {
                            ServiceDate = DateTime.Now,
                            ServiceDateSpecified = true,
                            Amount = findProduct.UnitPrice.Amount,
                            Taxable = true,
                            TaxableSpecified = true,
                            Id = new IdType
                                {
                                    Value = "1",
                                    idDomain = idDomainEnum.QBO,
                                },
                            AmountSpecified = true,
                            Desc = findProduct.Desc,
                            ItemsElementName = new ItemsChoiceType2[3],
                            Items = new object[3],
                        };


                    salere.Line[0].ItemsElementName[0] = ItemsChoiceType2.ItemId;

                    salere.Line[0].ItemsElementName[1] = ItemsChoiceType2.UnitPrice;

                    salere.Line[0].ItemsElementName[2] = ItemsChoiceType2.Qty;

                    salere.Line[0].Items[0] = new IdType
                        {
                            Value = findProduct.Id.Value,
                            idDomain = idDomainEnum.NG
                        };

                    salere.Line[0].Items[1] = findProduct.UnitPrice.Amount;

                    salere.Line[0].Items[2] = (decimal) 1;

                    dataServices.Add(salere);
                }
                

                if (customer.SelectedPackage.Period.Equals("1 month") && customer.OneTimeSetUpFeed)
                {
                    salere.Line = new SalesReceiptLine[2];

                    //LINE 1
                    salere.Line[0] = new SalesReceiptLine
                    {
                        ServiceDate = DateTime.Now,
                        ServiceDateSpecified = true,
                        Amount = findProduct.UnitPrice.Amount,
                        Taxable = true,
                        TaxableSpecified = true,
                        Id = new IdType
                        {
                            Value = "1",
                            idDomain = idDomainEnum.QBO,
                        },
                        AmountSpecified = true,
                        Desc = findProduct.Desc,
                        ItemsElementName = new ItemsChoiceType2[3],
                        Items = new object[3],
                    };


                    salere.Line[0].ItemsElementName[0] = ItemsChoiceType2.ItemId;

                    salere.Line[0].ItemsElementName[1] = ItemsChoiceType2.UnitPrice;

                    salere.Line[0].ItemsElementName[2] = ItemsChoiceType2.Qty;

                    salere.Line[0].Items[0] = new IdType
                    {
                        Value = findProduct.Id.Value,
                        idDomain = idDomainEnum.NG
                    };

                    salere.Line[0].Items[1] = findProduct.UnitPrice.Amount;

                    salere.Line[0].Items[2] = (decimal)1;

                    //LINE 2
                    findProductDataFeed = FindProduct(GetDataFeedPackage().QuickBookPackageName);

                    salere.Line[1] = new SalesReceiptLine
                    {
                        ServiceDate = DateTime.Now,
                        ServiceDateSpecified = true,
                        Amount = findProductDataFeed.UnitPrice.Amount,
                        Taxable = true,
                        TaxableSpecified = true,
                        Id = new IdType
                        {
                            Value = "2",
                            idDomain = idDomainEnum.QBO,
                        },
                        AmountSpecified = true,
                        Desc = findProductDataFeed.Desc,
                        ItemsElementName = new ItemsChoiceType2[3],
                        Items = new object[3],
                    };


                    salere.Line[1].ItemsElementName[0] = ItemsChoiceType2.ItemId;

                    salere.Line[1].ItemsElementName[1] = ItemsChoiceType2.UnitPrice;

                    salere.Line[1].ItemsElementName[2] = ItemsChoiceType2.Qty;

                    salere.Line[1].Items[0] = new IdType
                    {
                        Value = findProductDataFeed.Id.Value,
                        idDomain = idDomainEnum.NG
                    };

                    salere.Line[1].Items[1] = findProductDataFeed.UnitPrice.Amount;

                    salere.Line[1].Items[2] = (decimal)1;

                    dataServices.Add(salere);
                }

                //---------------------------------------MAKE PAYMENT------------------------------------------

                var payment = new Intuit.Ipp.Data.Qbo.Payment
                {
                    Synchronized = false,
                    SynchronizedSpecified = false,
                    MetaData = new ModificationMetaData
                    {
                        CreateTime = DateTime.Now,
                        CreateTimeSpecified = true,
                        CreatedBy = "POSTCLAdmin",
                        LastUpdatedTime = DateTime.Now,
                        LastUpdatedTimeSpecified = true,
                    },


                    Header = new PaymentHeader
                    {
                        CustomerId = new IdType
                        {
                            Value = findCustomer.Id.Value,
                            idDomain = findCustomer.Id.idDomain
                        },
                        CustomerName = findCustomer.Name,
                        Currency = currencyCode.USD,
                        CurrencySpecified = true,
                        ProcessPayment = true,
                        ProcessPaymentSpecified = true,
                        TotalAmt = findProduct.UnitPrice.Amount,
                        TotalAmtSpecified = true,
                        TxnDate = DateTime.Now,
                        Detail = new PaymentDetail()
                        {

                        },
                    },




                };

                

                if (customer.OneTimeSetUpFeed)
                {
                    if (findProductDataFeed != null)
                        payment.Header.TotalAmt = payment.Header.TotalAmt + findProductDataFeed.UnitPrice.Amount;
                }

                var creditTransaction = new CreditCardPayment
                {
                    CreditChargeInfo = new CreditChargeInfo
                    {
                        BillAddrStreet = customer.BillingStreetAddress,
                        Number = customer.CardNumber,
                        NameOnAcct = customer.NameOnCard,
                        CcExpirMn = customer.ExpirationMonth,
                        CcExpirYr = customer.ExpirationYear,
                        ZipCode = customer.BillingZipCode,
                        CCTxnMode = CCTxnModeEnum.CardNotPresent,
                        CCTxnType = CCTxnTypeEnum.Charge,
                        Cvv = customer.SecurityCode,
                        TypeSpecified = true,
                        Token = "0fe63cdbb9126b4a6bb81c0b13434fd2bb88",
                        CCTxnModeSpecified = true,
                        CCTxnTypeSpecified = true,
                        CcExpirMnSpecified = true,
                        CcExpirYrSpecified = true,
                        Type = CreditCardTypeEnum.Visa,

                    },
                    CreditChargeResponse = new CreditChargeResponse
                    {
                    },
                };

                if (customer.CreditCardType.Equals(CreditCardHelper.CardType.Amex))
                {
                    payment.Header.PaymentMethodId = new IdType()
                    {
                        Value = "6",
                        idDomain = idDomainEnum.QBO,
                    };

                    payment.Header.PaymentMethodName = "American Express";
                    {


                    };

                    creditTransaction.CreditChargeInfo.Type = CreditCardTypeEnum.AmEx;

                }
                else if (customer.CreditCardType.Equals(CreditCardHelper.CardType.Discover))
                {
                    payment.Header.PaymentMethodId = new IdType()
                    {
                        Value = "5",
                        idDomain = idDomainEnum.QBO,
                    };

                    payment.Header.PaymentMethodName = "Discover";
                    {

                    };

                    creditTransaction.CreditChargeInfo.Type = CreditCardTypeEnum.Discover;
                }
                else if (customer.CreditCardType.Equals(CreditCardHelper.CardType.MasterCard))
                {
                    payment.Header.PaymentMethodId = new IdType()
                    {
                        Value = "4",
                        idDomain = idDomainEnum.QBO,
                    };

                    payment.Header.PaymentMethodName = "Master Card";
                    {

                    };

                    creditTransaction.CreditChargeInfo.Type = CreditCardTypeEnum.MasterCard;
                }
                else if (customer.CreditCardType.Equals(CreditCardHelper.CardType.VISA))
                {
                    payment.Header.PaymentMethodId = new IdType()
                    {
                        Value = "3",
                        idDomain = idDomainEnum.QBO,
                    };

                    payment.Header.PaymentMethodName = "Visa";
                    {

                    };

                    creditTransaction.CreditChargeInfo.Type = CreditCardTypeEnum.Visa;
                }

                payment.Header.Detail = new PaymentDetail { Item = creditTransaction };
                
                dataServices.Add(payment);
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return false;
            }

          
        }

        public static void MakePayment(BillingCustomer customer)
        {
            var oauthValidator = new OAuthRequestValidator(
                 ConfigurationManager.AppSettings["accessToken"].ToString(CultureInfo.InvariantCulture),
                 ConfigurationManager.AppSettings["accessTokenSecret"].ToString(CultureInfo.InvariantCulture),
                 ConfigurationManager.AppSettings["consumerKey"].ToString(CultureInfo.InvariantCulture),
                 ConfigurationManager.AppSettings["consumerKeySecret"].ToString(CultureInfo.InvariantCulture));

            var context = new ServiceContext(
                oauthValidator,
                ConfigurationManager.AppSettings["realmId"].ToString(CultureInfo.InvariantCulture),
                IntuitServicesType.QBO);

            
            var dataServices = new DataServices(context);


            var findCustomer = FindCustomer(customer.QuickBookAccountName);

            var salere = new Payment
            {
                Synchronized = false,
                SynchronizedSpecified = false,
                MetaData = new ModificationMetaData
                {
                    CreateTime = DateTime.Now,
                    CreateTimeSpecified = true,
                    CreatedBy = "VinAdmin",
                    LastUpdatedTime = DateTime.Now,
                    LastUpdatedTimeSpecified = true,
                },
                Header = new PaymentHeader
                {
                    CustomerId = new IdType
                    {
                        Value = findCustomer.Id.Value,
                        idDomain = findCustomer.Id.idDomain
                    },
                    CustomerName = findCustomer.Name,
                    Currency = currencyCode.USD,
                    CurrencySpecified = true,
                    ProcessPayment = true,
                    ProcessPaymentSpecified = true,
                    TotalAmt = 1,
                    TotalAmtSpecified = true,
                    Detail = new PaymentDetail
                    {
                    },
                    PaymentMethodId = new IdType
                    {
                        Value = "5",
                        idDomain = idDomainEnum.QBO,
                    },
                    PaymentMethodName = "Discover",
                    TxnDate = DateTime.Now
                },
            };

            var creditTransaction = new CreditCardPayment
            {
                CreditChargeInfo = new CreditChargeInfo
                {
                    BillAddrStreet = customer.BillingStreetAddress,
                    Number = customer.CardNumber,
                    //Type = CreditCardTypeEnum.Discover,
                    NameOnAcct = customer.NameOnCard,
                    CcExpirMn = customer.ExpirationMonth,
                    CcExpirYr = customer.ExpirationYear,
                    ZipCode = customer.BillingZipCode,
                    CCTxnMode = CCTxnModeEnum.CardNotPresent,
                    CCTxnType = CCTxnTypeEnum.Charge,
                    Cvv = customer.SecurityCode,
                    TypeSpecified = true,
                    Token = "0fe63cdbb9126b4a6bb81c0b13434fd2bb88",
                    CCTxnModeSpecified = true,
                    CCTxnTypeSpecified = true,
                    CcExpirMnSpecified = true,
                    CcExpirYrSpecified = true,
                },
                CreditChargeResponse = new CreditChargeResponse
                {
                },
            };

            salere.Header.Detail = new PaymentDetail { Item = creditTransaction };

            dataServices.Add(salere);

       
        }

        public static Customer FindCustomer(string name)
        {
            var oauthValidator = new OAuthRequestValidator(
             ConfigurationManager.AppSettings["accessToken"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["accessTokenSecret"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["consumerKey"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["consumerKeySecret"].ToString(CultureInfo.InvariantCulture));

            var context = new ServiceContext(
                oauthValidator,
                ConfigurationManager.AppSettings["realmId"].ToString(CultureInfo.InvariantCulture),
                IntuitServicesType.QBO);


         


            var customerQuery = new CustomerQuery
            {
                Name = name
            };

            var findCustomer = customerQuery.ExecuteQuery<Customer>(context).First<Customer>();


            return findCustomer;

        }

      


        public static Item FindProduct(string name)
        {
            var oauthValidator = new OAuthRequestValidator(
             ConfigurationManager.AppSettings["accessToken"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["accessTokenSecret"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["consumerKey"].ToString(CultureInfo.InvariantCulture),
             ConfigurationManager.AppSettings["consumerKeySecret"].ToString(CultureInfo.InvariantCulture));

            var context = new ServiceContext(
                oauthValidator,
                ConfigurationManager.AppSettings["realmId"].ToString(CultureInfo.InvariantCulture),
                IntuitServicesType.QBO);
            var productQuery = new ItemQuery
            {
                Name = name
            };

            var findProduct = productQuery.ExecuteQuery<Item>(context).First<Item>();

            return findProduct;

        }

        public static IEnumerable<ExpirationMonth> GetExpirationMonths()
        {
            var returnList = new List<ExpirationMonth>();

            for (var i = 1; i <= 12; i++)
            {
                string strMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);

                var expireMonth = new ExpirationMonth()
                    {
                        Month = i,
                        MonthName = strMonthName,
                    };

                returnList.Add(expireMonth);
            }

            return returnList;
        }

        public static IEnumerable<int> GetExpirationYears()
        {
            var returnList = new List<int>();

            for (var i = DateTime.Now.Year; i < DateTime.Now.Year + 12; i++)
            {

                returnList.Add(i);
            }

            return returnList;
        }

        public static IEnumerable<LeadFormat> GetLeadFormats()
        {
            var returnList = new List<LeadFormat>();

            var leadFormatAdf = new LeadFormat()
                {
                    LeadId = 1,
                    LeadFormatName = "ADF"
                };


            var leadFormatText = new LeadFormat()
            {
                LeadId = 2,
                LeadFormatName = "Text"
            };

            var leadFormatBoth = new LeadFormat()
            {
                LeadId = 3,
                LeadFormatName = "Both"
            };

            returnList.Add(leadFormatText);
            returnList.Add(leadFormatAdf);
            returnList.Add(leadFormatBoth);

            return returnList;
        }

        public static IEnumerable<PostClPackgae> GetAllPackages()
        {
            using (var context = new CLDMSEntities())
            {
                return context.Packages.Where(x=>x.MonthlyPrice > 0).Select(x => new PostClPackgae()
                    {
                        PackageId = x.PackageId,
                        MonthlyPrice = x.MonthlyPrice.Value,
                        PackageName = x.PackageName,
                        Period = x.Period,
                        QuickBookPackageId = x.QuickBookPackageId.Value,
                        QuickBookPackageName = x.QuickBookPackageName,
                        TotalCharge = x.TotalCharge.Value,

                    }

                    ).ToList();
            }


        }

        public static PostClPackgae GetDataFeedPackage()
        {
            using (var context = new CLDMSEntities())
            {
                return context.Packages.Where(x => x.MonthlyPrice == 0).Select(x => new PostClPackgae()
                    {
                        PackageId = x.PackageId,
                        MonthlyPrice = x.MonthlyPrice.Value,
                        PackageName = x.PackageName,
                        Period = x.Period,
                        QuickBookPackageId = x.QuickBookPackageId.Value,
                        QuickBookPackageName = x.QuickBookPackageName,
                        TotalCharge = x.TotalCharge.Value,

                    }

                    ).First();
            }


        }

        public static int GetTotalCharge(BillingCustomer customer)
        {

            if (customer.SelectedPackage.Period.Equals("1 month"))
            {
                if (customer.OneTimeSetUpFeed)
                    return customer.SelectedPackage.TotalCharge + 59;
            }

            return customer.SelectedPackage.TotalCharge;
        }


    }


}

