using System.Collections.Generic;
using System.Linq;
using VinCLAPP.DatabaseModel;

namespace VinCLAPP.Helper
{
    public class ZipCodeHelper
    {
        public static ZipCodeCity LookUpZipCode(int zipCode)
        {
            var context = new CLDMSEntities();

            if (context.UsaZipCodes.Any(x => x.UsaZipCodeId == zipCode))
            {
                var findResult = context.UsaZipCodes.FirstOrDefault(x => x.UsaZipCodeId == zipCode);
                return new ZipCodeCity
                    {
                        City = findResult.CityName,
                        State = findResult.StateName,
                        StateAbbr=findResult.StateAbbr
                    };
            }
            else
                return new ZipCodeCity();
        }

        public static IEnumerable<UsState> GetAllStates()
        {
            var context = new CLDMSEntities();

            return context.UsaZipCodes.Select(x => new UsState()
                {
                    State = x.StateName,
                    StateAbbr = x.StateAbbr,

                }
                ).Distinct().OrderBy(x => x.State).ToList();

        }
    }

    public class ZipCodeCity
    {
        public string City { get; set; }

        public string State { get; set; }

        public string StateAbbr { get; set; }

    }

    public class UsState
    {
        public string State { get; set; }

        public string StateAbbr { get; set; }

        public override string ToString()
        {
            return State;
        }
    }
}