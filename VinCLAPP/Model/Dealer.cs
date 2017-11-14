using System;
using System.Collections.Generic;
using Vinclapp.Craigslist;

namespace VinCLAPP.Model
{
    public class Dealer
    {
        public List<EmailAccount> EmailAccountList;
        
        public int DealerId { get; set; }

        public int VincontrolId { get; set; }

        public string Password { get; set; }

        public string DealershipName { get; set; }

        public string PhoneNumber { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string LogoUrl { get; set; }

        public string WebSiteUrl { get; set; }

        public string CreditUrl { get; set; }

        public string LeadEmail { get; set; }

        public string CityOveride { get; set; }

        public string DealerTitle { get; set; }

        public string EndingSentence { get; set; }

        public bool PostWithPrice { get; set; }

        public int EmailFormat { get; set; }

        public int DelayTimer { get; set; }

        public List<VehicleInfo> Inventory { get; set; }

        public List<SimpleVehicleInfo> SimpleInventory { get; set; }

        public List<City> CityList { get; set; }
    }


 

    //public class VehicleInfo
    //{
    //    public int AutoId { get; set; }

    //    public int ListingId { get; set; }

    //    public string StockNumber { get; set; }

    //    public string Vin { get; set; }

    //    public string ModelYear { get; set; }

    //    public string Make { get; set; }

    //    public string Model { get; set; }

    //    public string Trim { get; set; }

    //    public string Cylinder { get; set; }

    //    public string BodyType { get; set; }

    //    public int SalePrice { get; set; }

    //    public string ExteriorColor { get; set; }

    //    public string InteriorColor { get; set; }

    //    public string Mileage { get; set; }

    //    public string Description { get; set; }

    //    public string CarImageUrl { get; set; }

    //    public string Door { get; set; }

    //    public string Fuel { get; set; }

    //    public string Litters { get; set; }

    //    public string Tranmission { get; set; }

    //    public string WheelDrive { get; set; }

    //    public string Engine { get; set; }

    //    public string Options { get; set; }

    //    public string DefaultImageUrl { get; set; }

    //    public string HtmlImageUrl { get; set; }

    //    public string FirstImageUrl { get; set; }

    //    public int TrackingId { get; set; }

    //    public int PostingCityId { get; set; }

    //    public string CraigslistAccountName { get; set; }

    //    public string CraigslistAccountPassword { get; set; }

    //    public string CraigslistAccountPhone { get; set; }

    //    public long ClPostingId { get; set; }
        
    //    public int AdTrackingId { get; set; }

    //    public int DealerId { get; set; }

    //    public string CraigslistCityUrl { get; set; }

    //    public string DealershipName { get; set; }

    //    public string PhoneNumber { get; set; }

    //    public string StreetAddress { get; set; }

    //    public string City { get; set; }

    //    public string State { get; set; }

    //    public string ZipCode { get; set; }

    //    public int Pictures { get; set; }

    //    public int TotalAds { get; set; }
    //    public int LastPosted { get; set; }
    //}


    public class SimpleVehicleInfo
    {
        public bool IsSelected { get; set; }

        public int ListingId { get; set; }

        public int AutoId { get; set; }

        public string PostingCity { get; set; }

        public int PostingCityId { get; set; }

        public string Stock { get; set; }

        public string Vin { get; set; }

        public string LastPosted { get; set; }

        public string Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public string ExteriorColor { get; set; }

        public int Pictures { get; set; }

        public string Mileage { get; set; }

        public int SalePrice { get; set; }

        public string AdsLink { get; set; }

        public int TotalAds { get; set; }

        public string Condition { get; set; }
    }

    public class LeadFormat
    {
        public int LeadId { get; set; }

        public string LeadFormatName { get; set; }

        public override string ToString()
        {
            return LeadFormatName;
        }
    }
}