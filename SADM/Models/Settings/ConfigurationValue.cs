using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace SADM.Models.Settings
{
    [Preserve(AllMembers = true)]
    public class ConfigurationValue
    {
        public string BaseUrl { get; set; }
        public string OauthUserName { get; set; }
        public string OauthPassword { get; set; }
        public string OauthGrantType { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public IList<PaymentCenter> PaymentCenterList { get; set; }
    }
}