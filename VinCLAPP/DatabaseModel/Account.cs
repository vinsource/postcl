//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VinCLAPP.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.SelectedCities = new HashSet<SelectedCity>();
            this.Trackings = new HashSet<Tracking>();
        }
    
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalPhone { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public Nullable<int> DealerId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<int> Quota { get; set; }
        public Nullable<int> FirstCity { get; set; }
        public string Ip { get; set; }
        public Nullable<System.DateTime> TrialStartDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string QuickbookAccountId { get; set; }
        public string QuickBookAccountName { get; set; }
        public Nullable<int> DailyLimit { get; set; }
        public string APIUsername { get; set; }
        public string APIPassword { get; set; }
        public string APIAccountId { get; set; }
        public Nullable<bool> IsFirstLogOn { get; set; }
        public Nullable<bool> DataFeedSetUp { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Trial { get; set; }
    
        public virtual ICollection<SelectedCity> SelectedCities { get; set; }
        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
