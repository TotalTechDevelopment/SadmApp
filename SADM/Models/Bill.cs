using System.Windows.Input;
using Newtonsoft.Json;

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

        public string Nis { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }


        public bool SendToTheAddress { get; set; }
        public bool SendToEmail { get; set; }
        public ICommand DownloadCommand { get; set; }
        public ICommand SendToTheAddressCommand { get; set; }
        public ICommand SendToEmailCommand { get; set; }
    }
}
