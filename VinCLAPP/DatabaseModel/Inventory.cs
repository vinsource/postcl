//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VinCLAPP.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public int ListingID { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string VINNumber { get; set; }
        public string StockNumber { get; set; }
        public string SalePrice { get; set; }
        public string MSRP { get; set; }
        public string Mileage { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string InteriorSurface { get; set; }
        public string BodyType { get; set; }
        public string Cylinders { get; set; }
        public string Liters { get; set; }
        public string EngineType { get; set; }
        public string DriveTrain { get; set; }
        public string FuelType { get; set; }
        public string Tranmission { get; set; }
        public string Doors { get; set; }
        public string CarsPackages { get; set; }
        public string CarsOptions { get; set; }
        public string Descriptions { get; set; }
        public string CarImageUrl { get; set; }
        public string ThumbnailImageURL { get; set; }
        public Nullable<System.DateTime> DateInStock { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<int> DealerId { get; set; }
        public string DealershipName { get; set; }
        public string DealershipAddress { get; set; }
        public string DealershipCity { get; set; }
        public string DealershipState { get; set; }
        public string DealershipPhone { get; set; }
        public string DealershipZipCode { get; set; }
        public string DealerCost { get; set; }
        public string ACV { get; set; }
        public string DefaultImageUrl { get; set; }
        public string NewUsed { get; set; }
        public string AddToInventoryBy { get; set; }
        public Nullable<System.DateTime> LastPosted { get; set; }
        public Nullable<bool> Certified { get; set; }
        public Nullable<int> VincontrolListingId { get; set; }
    
        public virtual Dealer Dealer { get; set; }
    }
}
