using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataFeedsVinControlConversion.Helper
{
    public class CarStockURL
    {
        public string Stock { get; set; }

        public string ImageURL { get; set; }

        public Byte[] ImageBytes { get; set; }

        public string ImageUrlNohosting { get; set; }

        public DataRow ImageRow { get; set; }

        
    }
}
