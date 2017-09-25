using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp.Model
{
    public class CraigsListTrackingModel
    {
        public string HtmlCraigslistUrl { get; set; }

        public long CLPostingId { get; set; }

        public int ListingId { get; set; }

        public int DealerId { get; set; }

        public int CityId { get; set; }

        public int Computer { get; set; }

        public string EmailAccount { get; set; }

      }
}
