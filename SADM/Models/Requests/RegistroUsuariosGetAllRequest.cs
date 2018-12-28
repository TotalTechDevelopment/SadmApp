using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models.Requests
{
    public class RegistroUsuariosGetAllRequest : RequestBase
    {
        [Refit.AliasAs("where")]
        public string where { get; set; }
        [Refit.AliasAs("order")]
        public string order { get; set; }
    }
}
