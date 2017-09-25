using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp.Helper
{
    public class CarStockURL
    {
        public string Stock { get; set; }

        public string ImageURL { get; set; }

        public Byte[] ImageBytes { get; set; }

        public string ImageUrlNohosting { get; set; }

        public WhitmanEntepriseVehicleInfo ImageRow { get; set; }

        
    }
}
