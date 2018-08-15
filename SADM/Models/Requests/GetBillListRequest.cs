namespace SADM.Models.Requests
{
    public class GetBillListRequest : RequestBase
    {
        [Refit.AliasAs("email")]
        public string Email { get; set; }
    }
}