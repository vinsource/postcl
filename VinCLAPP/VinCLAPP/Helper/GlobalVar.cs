using System;
using System.Collections.Generic;
using VinCLAPP.AutomativeDescriptionService7;
using VinCLAPP.Model;

namespace VinCLAPP
{
    public class GlobalVar
    {
        //STRING VARIABLES
        public static List<ChunkString> ChunkList;

        public static List<IdentifiedString> LuxuryMakesList;

        public static List<PostClDriveTrain> DriveTrainList;
        
        public static Account CurrentAccount;

        public static Dealer CurrentDealer;

        public static TrialInfo TrialInfo;
    }

    public class TrialInfo
    {
        public DateTime? StartUsingTime { get; set; }
        public bool IsTrial { get; set; }
    }
}