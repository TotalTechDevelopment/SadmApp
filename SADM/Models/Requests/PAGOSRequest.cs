using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models.Requests
{
    public class PAGOSRequest : RequestBase
    {
        [JsonProperty("v_f_fact")]
        public string v_f_fact { get; set; }
        [JsonProperty("v_nis_rad")]
        public int v_nis_rad { get; set; }
        [JsonProperty("v_sec_nis")]
        public int v_sec_nis { get; set; }
        [JsonProperty("v_sec_rec")]
        public int v_sec_rec { get; set; }
        [JsonProperty("v_referencia")]
        public string v_referencia { get; set; }
        [JsonProperty("v_importe")]
        public float v_importe { get; set; }
        [JsonProperty("v_fecha_vencimiento")]
        public DateTime v_fecha_vencimiento { get; set; }
    }
}
