using System.Collections.Generic;
using ClappManagementSystem.Model;

namespace ClappManagementSystem.Helper
{
    public class Globalvar
    {
       public static List<Dealer> DealerList { get; set; }
       
        public static List<City> CityList { get; set; }

        public static List<Computer> ComputerList { get; set; }

        public static List<TrackingDealer> TrackingList { get; set; }

        public static List<GridScheuleModel> GridScheduleList { get; set; }

        public static List<WhitmanEntepriseMasterVehicleInfo> BufferMasterVehicleList { get; set; }

        public static int SplitSchedules=1;



    }
}
