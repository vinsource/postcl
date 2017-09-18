namespace VinCLAPP.Helper
{
    public static class CraigsListHelper
    {
        public static int DetecStatusFromURL(string URL)
        {
            /*
             * 0:NORMAL
             * 1:RAPID
             * 2:VERIFY PHONE NUMBER
             * 
             */
            int result = 0;
            if (URL.Contains("s=postcount"))
                result = 1;
            else if (URL.Contains("s=pn"))
                result = 2;
            else if (URL.Contains("s=mailoop"))
                result = 3;
            return result;
        }
    }
}