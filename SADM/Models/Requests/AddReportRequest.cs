using System;
using Newtonsoft.Json;
using SADM.Enums;

namespace SADM.Models.Requests
{
    public class AddReportRequest : RequestBase
    {
        public ReportType Type { get; set; }

        public String Nis { get; set; }

        public Report Report { get; set; }
    }
}
