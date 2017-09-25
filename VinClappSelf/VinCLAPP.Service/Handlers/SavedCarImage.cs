using System.Runtime.Serialization;

namespace VinCLAPP.Service.Handlers
{
    [DataContract]
    public class SavedCarImage
    {
        [DataMember]
        public string ThumbnailUrLs { get; set; }
        [DataMember]
        public string FileUrLs { get; set; }
    }
}