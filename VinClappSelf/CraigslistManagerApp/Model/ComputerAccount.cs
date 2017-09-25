using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp.Model
{
    public class ComputerAccount
    {
        public int ComputerId { get; set; }

        public int DealerId { get; set; }

        public int CityId { get; set; }

        public bool Price { get; set; }

        public string CityName { get; set; }

        public string CraigsListCityURL { get; set; }

        public bool SubCity { get; set; }

        public int CLIndex { get; set; }

        public string FTPUserName { get; set; }

        public string FTPPassword { get; set; }

    }
}
