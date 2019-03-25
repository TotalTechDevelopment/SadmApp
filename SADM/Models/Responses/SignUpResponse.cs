using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class SignUpResponse : ResponseBase
    {
        public string Folio { get; set; }
        [JsonProperty("Error")]
        public string ErrorMessage { get; set; }
    }
}
