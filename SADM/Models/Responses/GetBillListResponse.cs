using System.Collections.Generic;
using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class GetBillListResponse : ResponseBase
    {
        [JsonProperty("...")]
        public IList<Bill> BillList { get; set; }
    }
}