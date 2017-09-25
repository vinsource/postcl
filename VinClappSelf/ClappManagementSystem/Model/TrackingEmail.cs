﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClappManagementSystem.Model
{
    public class TrackingEmail
    {
        public string Email { get; set; }

        public int Computer { get; set; }

        public int PostedAds { get; set; }

        public int ShowedAds { get; set; }

        public string SuccessfulRate { get; set; }
    }
}
