using System.Runtime.Serialization;

namespace VinCLAPP.Service.Handlers
{
    [DataContract]
    public class ServerImage
    {
        [DataMember]
        public string FileUrl { get; set; }
        [DataMember]
        public string ThumbnailUrl { get; set; }
    }
}