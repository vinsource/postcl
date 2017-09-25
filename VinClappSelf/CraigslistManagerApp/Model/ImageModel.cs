using System.Collections.Generic;

namespace CraigslistManagerApp.Model
{
    public class ImageModel
    {

        public string AdsId { get; set; }

        public string TestingHtml { get; set; }

        public byte[] TopImage { get; set; }

        public byte[] BottomImage { get; set; }

        public List<string> PhysicalImageUrl { get; set; }
    }
}
