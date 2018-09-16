using Newtonsoft.Json;

namespace SADM.Models.Requests
{
    public class RemoveContractRequest : RequestBase
    {
        [JsonProperty("EMAIL")]
        public string Email { get; set; }
        [JsonProperty("LECT")]
        public string PreviousReading { get; set; }
        [JsonProperty("ID_USUARIO")]
        public long UserId { get; set; }
        [JsonIgnore]
        public string Nir { get; set; }

        [JsonProperty("SEC_REC")]
        public string SecRec;
        [JsonProperty("NIS_RAD")]
        public string NisRad;
        [JsonProperty("SEC_NIS")]
        public string Nis;
        [JsonProperty("F_FACT")]
        public string Date;

        //Constant
        [JsonProperty("ab")]
        public string Vab => "B";
    }
}
