using System;

namespace VinCLAPP.Helper
{
    public class TrialHelper
    {
        public static bool IsValidTrial(bool isTrial, DateTime startUsingTime)
        {
            var resetDateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var resetstartUsingTime = new DateTime(startUsingTime.Year, startUsingTime.Month, startUsingTime.Day);

            return !isTrial || (resetDateNow - resetstartUsingTime).Days <= 1;
        }

        public static int TrialDaysLeft()
        {
            var resetDateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var resetstartUsingTime = new DateTime(GlobalVar.TrialInfo.StartUsingTime.Value.Year, GlobalVar.TrialInfo.StartUsingTime.Value.Month, GlobalVar.TrialInfo.StartUsingTime.Value.Day);

            return 1 - (resetDateNow.Subtract(resetstartUsingTime)).Days;
                    
        }


    }
}