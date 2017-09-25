using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFeedsVinControlConversion.Helper
{
    public class VinControlVehicle
    {
        public int ListingId { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public string VIN { get; set; }

        public int DaysInInventory { get; set; }

        public string StockNumber { get; set; }

        public string SalePrice { get; set; }

        public string MSRP { get; set; }

        public string Mileage { get; set; }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public string InteriorSurface { get; set; }

        public string BodyType { get; set; }

        public string EngineType { get; set; }

        public string DriveTrain { get; set; }

        public string Cylinders { get; set; }

        public string Liters { get; set; }

        public string FuelType { get; set; }

        public string Tranmission { get; set; }

        public string Doors { get; set; }

        public bool Certified { get; set; }

        public string CarsOptions { get; set; }

        public string Descriptions { get; set; }

        public string CarImageUrl { get; set; }

        public string ThumbnalImageurl { get; set; }

        public DateTime DateInStock { get; set; }

        public string DealershipName { get; set; }

        public string DealershipAddress { get; set; }

        public string DealershipCity { get; set; }

        public string DealershipState { get; set; }

        public string DealershipZipCode { get; set; }

        public string DealershipPhone { get; set; }

        public int DealerId { get; set; }

        public string DealerCost { get; set; }

        public string ACV { get; set; }

        public string DefaultImageUrl { get; set; }

        public string NewUsed { get; set; }

        public int Age { get; set; }

        public string AddToInventoryBy { get; set; }

        public string AppraisalId { get; set; }

        public string DefaultImageURL { get; set; }

        public string FuelEconomyCity { get; set; }

        public string FuelEconomyHighWay { get; set; }
        public string VehicleType { get; set; }
        public string TruckClass { get; set; }
        public string TruckCategory { get; set; }
        public string TruckType { get; set; }
        public bool IsTruck { get; set; }
        public bool Recon { get; set; }
        public int VehicleStatus { get; set; }

        public string StandardOptions { get; set; }

        public string RetailPrice { get; set; }

        public string DealerDiscount { get; set; }

        public string WindowStickerPrice { get; set; }

        public string ManufacturerRebate { get; set; }

        public int CarFaxOwner { get; set; }
    }
}
