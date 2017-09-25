using System;
using System.Data;
using System.Collections.Generic;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp
{
    public class clsVariables
    {
        //STRING VARIABLES
        public static List<CraigsListEmailAccount> accountList;

        public static List<CraigsListEmailAccount> computeremailaccountList;

        public static List<WhitmanEnterpriseChunkString> chunkList;

        public static List<WhitmanHostingServer> hostingServerList;

        public static List<ComputerAccount> computerList;

        public static List<WhitmanEntepriseMasterVehicleInfo> currentMasterVehicleList;

        public static List<WhitmanEntepriseMasterVehicleInfo> fullMasterVehicleList;

        public static List<RenewScheduleModel> currentRenewList;

        public static WhitmanEnterpriseComputer currentComputer;

        public static WhitmanEnterpriseDealer currentDealer;

        public static int IntervalOfAds=2;

        public static int DelayTimer = 360000;

        public static int DelayTimerPostAccount = 360000;

        public static int DelayTimerRenew = 80000;

        public static bool PostAll = true;

        public static DateTime CurrentProcessingDate;

        public static bool Reload;

        public static bool ReloadRenew;

        public static string TextMessageLink = "http://www.freeautotext.com/dealers/clickthru.aspx?vin=PLACEVIN";
    

    }
}


