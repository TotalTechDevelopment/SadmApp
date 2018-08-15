using Newtonsoft.Json;

namespace SADM.Models.Requests
{
    public class AddContractRequest : RequestBase
    {
        [JsonProperty("EMAIL")]
        public string Email { get; set; }
        [JsonProperty("LECT")]
        public string PreviousReading { get; set; }
        [JsonProperty("ID_USUARIO")]
        public long UserId { get; set; }
        [JsonIgnore]
        public string Nir { get; set; }

        //Dynamic
        [JsonProperty("SEC_REC")]
        public string SecRec => Nir?.Substring(0, 1) ?? string.Empty;
        [JsonProperty("NIS_RAD")]
        public string NisRad => Nir?.Substring(1, 7) ?? string.Empty;
        [JsonProperty("SEC_NIS")]
        public string Nis => Nir?.Substring(8, 2) ?? string.Empty;
        [JsonProperty("F_FACT")]
        public string Date => Nir?.Substring(10, 8) ?? string.Empty;

        //Constant
        [JsonProperty("V_ab")]
        public string Vab => "A";
    }
}
