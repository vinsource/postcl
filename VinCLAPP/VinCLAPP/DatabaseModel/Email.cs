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
    
    public partial class Email
    {
        public int EmailAccountId { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> DealerId { get; set; }
        public Nullable<int> AccountAutoId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
