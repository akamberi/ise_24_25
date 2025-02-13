using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services
{
    namespace BLL.Services
    {
        public class PayPalSettings
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string Mode { get; set; } // "sandbox" or "live"
            public string ReturnUrl { get; set; }
            public string CancelUrl { get; set; }
        }
    }


}
