using System;

namespace VinCLAPP.Model
{
    public class Account
    {
        public int AccountId { get; set; }

        public int DailyLimit { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalPhone { get; set; }

        public string AccountName { get; set; }

        public string AccountPassword { get; set; }

        public int DealerId { get; set; }

        public bool Active { get; set; }

        public bool IsCurrentlyUsed { get; set; }

        public int Quota { get; set; }

        public bool IsFirstLogon { get; set; }

        public bool IsTrial { get; set; }

        public bool IsExist { get; set; }

        public bool SingleLogon { get; set; }

        public string DatabaseIp { get; set; }

        public string QuickBookAccountName { get; set; }

        public string QuickBookAccountId { get; set; }

        public string APIUsername { get; set; }

        public string APIPassword { get; set; }

        public string APIAccountId { get; set; }



        public DateTime TrialStartDate { get; set; }
    }
}