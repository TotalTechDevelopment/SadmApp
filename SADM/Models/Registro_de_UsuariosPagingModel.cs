using SADM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models
{
    public class Registro_de_UsuariosPagingModel
    {
        public List<LoginResponse> Registro_de_Usuarioss { set; get; }
        public int RowCount { set; get; }
    }
}
