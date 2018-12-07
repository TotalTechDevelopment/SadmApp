using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models
{
    public class Spartan_User_Role
    {
        public int User_Role_Id { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
         
        public virtual  Spartan_User_Role_Status Status_Spartan_User_Role_Status { get; set; }

    }
}
