using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClappManagementSystem.Model
{
    public class TrackingDealer
    {
        public int DealerId { get; set; }

        public string DealershipName { get; set; }

        public string City { get; set; }

        public bool ShowedAd { get; set; }

        public bool RenewAd { get; set; }

        public int NumberOfShowedAds { get; set; }

        public int NumberOfCars { get; set; }

        public int Computer { get; set; }

        public int TrackingId { get; set; }

        public string TradeInBannerLink { get; set; }
               
        public int CityId { get; set; }

        public string CityUrl { get; set; }
    }
}
