using System.Runtime.Serialization;

namespace Clapp.WpfControlLibrary.UploadImageControl.Models
{
    [DataContract]
    public class CarImage
    {
        [DataMember]
        public string ThumbnailUrLs { get; set; }
        [DataMember]
        public string FileUrLs { get; set; }
    }
}
