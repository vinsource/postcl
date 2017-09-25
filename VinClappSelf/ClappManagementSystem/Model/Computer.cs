using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClappManagementSystem.Model
{
    public class Computer
    {
        public int PcAccount { get; set; }

        public int DealerId { get; set; }

        public int CityId { get; set; }


        public override string ToString()
        {
            return "PC " + PcAccount;
        }
    }


    public class SimpleList
    {
        public int Computer { get; set; }

        public int Schedule { get; set; }

        public List<ComplicatedValueComboBox> SplitPart { get; set; }
    }

    public class ComplicatedValueComboBox
    {
        public int Text { get; set; }

        public int Value { get; set; }
    }

}
