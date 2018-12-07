using System;
using System.Collections.Generic;
using System.Text;

namespace SADM.Models
{
    public class Spartane_File
    {
        public int File_Id { get; set; }
        public string Description { get; set; }
        public int? File1 { get; set; }
        public int? File_Size { get; set; }
        public int? Object { get; set; }
        public int? User_Id { get; set; }
        public DateTime? Date_Time { get; set; }
        public virtual TTArchivos File1_TTArchivos { get; set; }
    }
}
