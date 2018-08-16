using System.Windows.Input;
using Newtonsoft.Json;
using SADM.Enums;

namespace SADM.Models
{
    public class Bill
    {
        [JsonProperty("f_Fact")]
        public string BillDate { get; set; }
        [JsonProperty("xml")]
        public string Xml { get; set; }
        [JsonProperty("pdf")]
        public string Pdf { get; set; }
        [JsonProperty("Nis")]
        public string Nis { get; set; }
    }
}