namespace SADM.Models.Requests
{
    public class GetContractListRequest : RequestBase
    {
        [Refit.AliasAs("email")]
        public string Email { get; set; }
    }
}