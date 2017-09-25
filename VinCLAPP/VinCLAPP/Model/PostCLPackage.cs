using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VinCLAPP.Model
{
    public class PostClPackage
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public int MonthlyPrice { get; set; }

        public string Period { get; set; }

        public int TotalCharge { get; set; }

        public int QuickBookPackageId { get; set; }

        public string QuickBookPackageName { get; set; }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return MonthlyPrice.ToString("c") + "/" +Period + " (Total : " + TotalCharge.ToString("c") + ")";
        }
    }

    public class PostClExpirationMonth
    {
        public int MonthNumber { get; set; }

        public string Month { get; set; }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Month;
        }
    }
}
