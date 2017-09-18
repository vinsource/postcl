using System.Windows.Threading;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class App
    {
        public static string ImageServiceURL { get; set; }

        public static int InventoryStatus { get; set; }

        public static int ListingId { get; set; }

        public static int DealerId { get; set; }

        public static string Vin { get; set; }

        public static Dispatcher Dispatcher { get; set; }
    }
}