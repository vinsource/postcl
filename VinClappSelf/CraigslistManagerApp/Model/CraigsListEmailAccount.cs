using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp
{
    public class CraigsListEmailAccount
    {
        public string CraigslistAccount { get; set; }

        public string CraigsListPassword { get; set; }

        public string CraigsAccountPhoneNumber { get; set; }

        public int IntervalofAds { get; set; }

        public bool isCurrentlyUsed { get; set; }

        public int Position { get; set; }

        public string Proxy { get; set; }

        public string DefaultGateWay { get; set; }

        public string Dns { get; set; }
    }
}
