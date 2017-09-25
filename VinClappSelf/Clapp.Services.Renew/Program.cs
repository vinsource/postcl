using System;
using System.Linq;
using Clapp.Services.Business.IEHelper;
using Clapp.Services.Business.Log;

namespace Clapp.Services.Renew
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //if (args.Length == 1)
            //{
                //var splitPart = Convert.ToInt32(args.FirstOrDefault());
                //SmsHelper.SendMessageToAtt("Craigslist Timing",
                //                           "Craigslist Part " + splitPart + " started at " + DateTime.Now.ToLongTimeString());
                RenewHelper.RenewAdsProcessByEmailAccount();
                //SmsHelper.SendMessageToAtt("Craigslist Timing",
                //                           "Craigslist Part " + splitPart + " ended at " + DateTime.Now.ToLongTimeString());
            //}
        }
    }
}
