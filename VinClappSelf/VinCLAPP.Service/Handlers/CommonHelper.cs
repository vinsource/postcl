using System;
using System.Linq;

namespace VinCLAPP.Service.Handlers
{
    public sealed class CommonHelper
    {
        public static string[] GetArrayFromString(string dataString)
        {
            return dataString == null || String.IsNullOrEmpty(dataString.Trim())
                       ? new[] {""}
                       : dataString.Split(new[] {',', '|'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}