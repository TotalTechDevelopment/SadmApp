using System.Collections.Generic;
using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class GetReportListResponse : ResponseBase
    {
        [JsonProperty("Reporte_de_Incidenciass")]
        public IList<Report> ReportList { get; set; }
    }
}
