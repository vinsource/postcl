using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp
{
    public class WhitmanEnterpriseDealer
    {
        public int DealerId { get; set; }

        public int VincontrolId { get; set; }

        public string Password { get; set; }

        public string DealershipName { get; set; }

        public string PhoneNumber { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string LogoURL { get; set; }

        public string WebSiteURL { get; set; }

        public string CreditURL { get; set; }

        public string Email { get; set; }

        public string CityOveride { get; set; }

        public int EmailFormat { get; set; }

        public List<WhitmanEnterpriseFTPAccount> FTPListAccount { get; set; }

        public List<WhitmanEntepriseVehicleInfo> Inventory { get; set; }

        public List<WhitmanEnterpriseCity> cityList { get; set; }

        
    }


    
  
}
