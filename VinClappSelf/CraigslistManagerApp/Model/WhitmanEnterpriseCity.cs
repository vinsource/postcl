using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraigslistManagerApp
{
    public class WhitmanEnterpriseCity
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public string CraigsListCityURL { get; set; }

        public bool SubCity { get; set; }

        public int CLIndex { get; set; }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return CityName;
        }
    }
}
