using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace VinCLAPP.Helper
{
    public static class ConfigHelper
    {
        private const string _smtpServer = "SMTPGoogleServer";
        private const string _defaultFromEmail = "DefaultFromEmail";
        private const string _displayName = "DisplayName";
        private const string _trackEmailPass = "TrackEmailPass";
        private const string _confirmationEmail = "ConfirmationEmail";
        private const string _confirmationfeedEmail = "ConfirmationFeedEmail";

        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings[_smtpServer];
            }
        }

        public static string DefaultFromEmail
        {
            get
            {
                return ConfigurationManager.AppSettings[_defaultFromEmail];
            }
        }

        public static string DisplayName
        {
            get
            {
                return ConfigurationManager.AppSettings[_displayName];
            }
        }

        public static string TrackEmailPass
        {
            get
            {
                return ConfigurationManager.AppSettings[_trackEmailPass];
            }
        }

        public static string ConfirmationEmail
        {
            get
            {
                return ConfigurationManager.AppSettings[_confirmationEmail];
            }
        }

        public static string ConfirmationFeedEmail
        {
            get
            {
                return ConfigurationManager.AppSettings[_confirmationfeedEmail];
            }
        }
    }
}
