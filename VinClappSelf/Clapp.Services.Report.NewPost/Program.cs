using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clapp.Services.Business.IEHelper;

namespace Clapp.Services.Report.NewPost
{
    internal class Program
    {
         [STAThread]
        static void Main(string[] args)
        {
            //var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //var dtCompare7AmRange = dtCompareToday.AddHours(07);

            //var dtCompare11P30MRange = dtCompareToday.AddHours(23).AddMinutes(30);

            //if (DateTime.Now.TimeOfDay >= dtCompare7AmRange.TimeOfDay && DateTime.Now.TimeOfDay <= dtCompare11P30MRange.TimeOfDay)
                ReportHelper.ReportProcessNewPost();
        }
    }
}
