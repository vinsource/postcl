using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clapp.Services.Business.IEHelper;

namespace Clapp.Services.ClickThrough
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ClickHelper.CloseInternetExplorers();
            ClickHelper.ClickThroughProcess();
        }
    }
}
