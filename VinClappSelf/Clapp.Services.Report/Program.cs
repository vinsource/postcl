using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clapp.Services.Business.IEHelper;

namespace Clapp.Services.Report
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //var dtCompare4AmRange = dtCompareToday.AddHours(04);

            //var dtCompare6PmRange = dtCompareToday.AddHours(18);

            //if (DateTime.Now.TimeOfDay >= dtCompare4AmRange.TimeOfDay && DateTime.Now.TimeOfDay <= dtCompare6PmRange.TimeOfDay)
                ReportHelper.ReportProcessRenew();
        }
    }
}
