using VinCLAPP.Helper;

namespace VinCLAPP.Model
{
    public class PostClCustomer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string DealerName { get; set; }

        public string QuickBookAccountName { get; set; }

        public string QuickBookAccountId { get; set; }

        public string WebSiteAddress { get; set; }

        public string DealerStreetAddress { get; set; }

        public string DealerZipCode { get; set; }

        public string DealerCity { get; set; }

        public string DealerState { get; set; }

        public string DealerPhone { get; set; }

        public string LeadEmail { get; set; }

        public string TemporaryPassword { get; set; }

        public int LeadFormat { get; set; }

        public int DailyLimit { get; set; }
    }

    public class BillingCustomer : PostClCustomer
    {
        public string BillingStreetAddress { get; set; }
       
        public string BillingCity { get; set; }

        public string BillingState { get; set; }
        
        public string BillingZipCode { get; set; }

        public string NameOnCard { get; set; }

        public string CardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string SecurityCode { get; set; }

        public bool OneTimeSetUpFeed { get; set; }

        public decimal TotalCharge { get; set; }

        public bool DifferentBillingAddress { get; set; }

        public PostClPackgae SelectedPackage { get; set; }

        public CreditCardHelper.CardType CreditCardType { get; set; }
    }

    public class PostClPackgae
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public int MonthlyPrice { get; set; }

        public string Period { get; set; }

        public int TotalCharge { get; set; }

        public int QuickBookPackageId { get; set; }

        public string QuickBookPackageName { get; set; }

        public override string ToString()
        {
            return MonthlyPrice.ToString("C") + "/" + Period + " (" + TotalCharge.ToString("C") + ")";
        }

       
    }

    public class PostClCity
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }


        public override string ToString()
        {
            return CityName + ", "+ StateName;
        }
    }

    

    public class ExpirationMonth
    {
        public int Month { get; set; }

        public string MonthName { get; set; }

        public override string ToString()
        {
            return MonthName;
        }
    }

    public class PostClDataFeed
    {
        public string VendorName { get; set; }

        public string VendorEmail { get; set; }

        public string VendorPhone { get; set; }

        public string CustomMessage { get; set; }

        public string YourName { get; set; }

    }
}