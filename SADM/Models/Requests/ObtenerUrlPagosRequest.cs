using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models.Requests
{
    public class ObtenerUrlPagosRequest : RequestBase
    {
        public string Amount { get; set; }
        public string returnUrl { get; set; }
        public string vpc_MerchTxtRef { get; set; }
        public string OrderInfo { get; set; }
    }
}
