namespace SADM.Models.Requests
{
    public class UpdateBillDeliveryConfigurationRequest : RequestBase
    {
        [Refit.AliasAs("email")]
        public string Email { get; set; }
        [Refit.AliasAs("indicador")]
        public int Indicator { get; set; }
        [Refit.AliasAs("nis_rad")]
        public string Nis { get; set; }
    }
}