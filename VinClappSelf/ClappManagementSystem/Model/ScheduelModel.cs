using System.Collections.Generic;

namespace ClappManagementSystem.Model
{
    public class ScheduelModel
    {
        public int PCNumber { get; set; }

        public int DealerId { get; set; }

        public string DealerName { get; set; }

        public string City { get; set; }

        public string Price { get; set; }
    }

    public class GridScheuleModel
    {
        public int Computer { get; set; }

        public int Cars { get; set; }

        public int Rounds { get; set; }

        public string TodayCreateAndRenew { get; set; }

        public string NextDayCreateAndRenew { get; set; }

        public string Next2DaysCreateAndRenew { get; set; }

        public IEnumerable<Dealer> DealerList { get; set; }
    }
}
