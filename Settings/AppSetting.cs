using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Settings
{
    public class AppSetting
    {
        public String? BaseUrl { get; set; }
        public String? Browser { get;  set; }
        public String? Username { get;  set; }
        public String? Password { get;  set; }
        public bool IsHeadlessMode { get;  set; }

    }
}
