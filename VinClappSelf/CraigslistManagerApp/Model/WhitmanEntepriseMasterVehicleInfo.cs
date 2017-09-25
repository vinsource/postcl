using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp.Model
{
    public class WhitmanEntepriseMasterVehicleInfo:ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int AutoID { get; set; }

        public int ListingId { get; set; }

        public string StockNumber { get; set; }

        public string Vin { get; set; }

        public string ModelYear { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public string Cylinder { get; set; }

        public string BodyType { get; set; }

        public string SalePrice { get; set; }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public string Mileage { get; set; }

        public string Description { get; set; }

        public string CarImageUrl { get; set; }

        public string Door { get; set; }

        public string Fuel { get; set; }

        public string Litters { get; set; }

        public string Tranmission { get; set; }

        public string WheelDrive { get; set; }

        public string Engine { get; set; }

        public string Options { get; set; }

        public string DefaultImageUrl { get; set; }

        public string HtmlImageUrl { get; set; }

        public int DealerId { get; set; }

        public string VincontrolId { get; set; }

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

        public string Email { get; set; }

        public string CityOveride { get; set; }

        public int EmailFormat { get; set; }

        public bool Price { get; set; }

        public int PostingCityId { get; set; }


        public string FirstImageUrl { get; set; }

        public string TradeInBannerLink { get; set; }

        public int TrackingId { get; set; }

        public ImageModel PostingImage { get; set; }

        public string CraigslistAccountName { get; set; }

        public string CraigslistAccountPassword { get; set; }

        public string CraigslistAccountPhone{ get; set; }

        public long CLPostingId { get; set; }
        public int AdTrackingId { get; set; }

        public bool CraigslistExist { get; set; }

        public string CraigslistUrl { get; set; }

        public string CraigslistCityUrl { get; set; }

        public string AddtionalTitle { get; set; }


    }
}
