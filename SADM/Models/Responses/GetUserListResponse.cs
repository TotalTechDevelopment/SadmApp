using System.Collections.Generic;
using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class GetUserListResponse : ResponseBase
    {
        [JsonProperty("Registro_de_Usuarioss")]
        public IList<User> UserList { get; set; }
    }
}
