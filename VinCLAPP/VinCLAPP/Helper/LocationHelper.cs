using System;
using System.IO;
using System.Reflection;

namespace VinCLAPP.Helper
{
    public class LocationHelper
    {
        public static string GetAppFolder()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName.Replace("\\bin\\Debug",String.Empty);
        }
    }
}