using SADM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models
{
    public class Spartan_User : ResponseBase
    {
        public int Id_User { get; set; }
        public string Name { get; set; }
        public int? Role { get; set; }
        public int? Image { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

   
        public virtual  Spartan_User_Role Role_Spartan_User_Role { get; set; }
   
        public virtual  Spartane_File Image_Spartane_File { get; set; }
      
        public virtual  Spartan_User_Status Status_Spartan_User_Status { get; set; }

    }
}
