using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp.Helper
{
    public class BufferVehicleModel
    {
        public int ListingID { get; set; }

        public string StockNumber { get; set; }

        public string VinNumber { get; set; }

        public string ModelYear { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public string Cylinders { get; set; }

        public string BodyType { get; set; }

        public string SalePrice { get; set; }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public string Mileage { get; set; }

        public string Descriptions { get; set; }

        public string CarImageUrl { get; set; }

        public string Doors { get; set; }

        public string FuelType { get; set; }

        public string Liters { get; set; }

        public string Tranmission { get; set; }

        public string DriveTrain { get; set; }

        public string EngineType { get; set; }

        public string CarsOptions { get; set; }

        public string DefaultImageUrl { get; set; }

        public string OverideTitle { get; set; }

        public int VincontrolId { get; set; }

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

        public string TradeInBannerLink { get; set; }

        public bool Price { get; set; }

        public int PostingCityId { get; set; }

        public int DealerId { get; set; }

        public int CityId { get; set; }

        public string FirstImageUrl { get; set; }

        public string AddtionalTitle { get; set; }

        public int Schedules { get; set; }
    }

    public class BufferMatch
    {
        public int CityId { get; set; }
        public int ListingID { get; set; }
    }
}
