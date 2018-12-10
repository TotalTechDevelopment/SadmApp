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
        public string vpc_Merchant { get; set; }
        public string vpc_AccessCode { get; set; }
        public string vpc_Command { get; set; }
        public string vpc_Version { get; set; }
        public string SecureSecret { get; set; }
        public string vpc_Locale { get; set; }
        public string vpc_Currency { get; set; }
        public string vpc_ReturnAuthResponseData { get; set; }
        public string vpc_SecureHashType { get; set; }
        public string PaymentBaseUrl { get; set; }
        public IList<PaymentCenter> PaymentCenterList { get; set; }
    }
}