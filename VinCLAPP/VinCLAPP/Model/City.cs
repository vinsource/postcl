namespace VinCLAPP.Model
{
    public class City
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public string CraigsListCityURL { get; set; }

        public bool SubCity { get; set; }

        public int CLIndex { get; set; }

        public bool isCurrentlyUsed { get; set; }

        public int Position { get; set; }

        public string AreaAbbr { get; set; }

        public string SubAbbr { get; set; }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return CityName;
        }
    }
}