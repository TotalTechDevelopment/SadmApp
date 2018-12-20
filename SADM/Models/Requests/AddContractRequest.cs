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
        public string SecRec => Nir.Split('-')[0];//Nir?.Substring(0, 1) ?? string.Empty;
        [JsonProperty("NIS_RAD")]
        public string NisRad => Nir.Split('-')[1];
        [JsonProperty("SEC_NIS")]
        public string Nis => int.Parse(Nir.Split('-')[2]).ToString();
        [JsonProperty("F_FACT")]
        public string Date => Nir.Split('-')[3];

        //Constant
        [JsonProperty("ab")]
        public string Vab => "A";
    }
}
