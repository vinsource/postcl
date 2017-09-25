using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClappManagementSystem.Model
{
    public class Dealer
    {
        public string DealershipName { get; set; }

        public string DealershipPhoneNumber { get; set; }

        public string DealershipFullAddress { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public int DealerId { get; set; }

        public string DealershipCarsComId { get; set; }

        public string DealershipAutoTraderId { get; set; }


        public string Latitude { get; set; }

        public string Longtitude { get; set; }

        public int ExceptionCategory { get; set; }

        public string DealerSpecialtyID { get; set; }

        public string SubConstraint { get; set; }

        public string DataFeedPath { get; set; }

        public int NumberOfCars { get; set; }

        public int Round { get; set; }

        public List<ScheduleCity> ScheduleCityList { get; set; }



        public override string ToString()
        {
            
                return DealerId + " - " + DealershipName + " - Cars = " + NumberOfCars + " / Rounds = " + Round;
          
        }
    }

    public class ScheduleCity
    {
        public int City{ get; set; }

        public bool Price{ get; set; }

        public bool Split{ get; set; }

        public int Schedules { get; set; }
    }
}
