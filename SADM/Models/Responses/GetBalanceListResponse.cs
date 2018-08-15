using System.Collections.Generic;
using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class GetBalanceListResponse : ResponseBase
    {
        [JsonProperty("SUMCONs")]
        public IList<Balance> BalanceList { get; set; }
    }
}