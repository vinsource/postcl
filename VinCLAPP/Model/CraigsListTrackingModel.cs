namespace VinCLAPP.Model
{
    public class CraigsListTrackingModel
    {
        public int AutoId { get; set; }

        public string HtmlCraigslistUrl { get; set; }

        public long ClPostingId { get; set; }

        public int ListingId { get; set; }

        public string StockNumber { get; set; }

        public string Title { get; set; }

        public int DealerId { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int Computer { get; set; }

        public string EmailAccount { get; set; }

        public string EmailPassword { get; set; }

        public int VinListingId { get; set; }
    }

    public class SimpleClTrackingModel
    {
        public int ListingId { get; set; }

        public string HtmlCraigslistUrl { get; set; }

       public string StockNumber { get; set; }

        public string Title { get; set; }
        public int TrackingId { get; set; }
       public string CityName { get; set; }
       public int CityId { get; set; }
     

     
    }
}