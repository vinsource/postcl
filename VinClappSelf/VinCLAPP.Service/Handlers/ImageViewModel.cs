namespace VinCLAPP.Service.Handlers
{
    public class ImageViewModel
    {
        public int ListingId { get; set; }

        public string Vin { get; set; }

        public int DealerId { get; set; }

        public string ImageUploadFiles { get; set; }

        public string ThumbnailImageUploadFiles { get; set; }

        public string CarImageUrl { get; set; }

        public int InventoryStatus { get; set; }

        public bool SessionTimeOut { get; set; }

    }
}