using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp.Model
{
    public class RenewScheduleModel
    {
        public int TrackingId { get; set; }

        public int ListingId { get; set; }

        public int CityId { get; set; }

        public string CraigslistAccountName { get; set; }

        public string CraigslistAccountPassword { get; set; }

        public string CraigslistAccountPhone { get; set; }

        public long CLPostingId { get; set; }


        public string CraigslistURL { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
