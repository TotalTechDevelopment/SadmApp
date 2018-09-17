namespace SADM.Models.Requests
{
    public class RecoverPasswordRequest : RequestBase
    {
        [Refit.AliasAs("email")]
        public string Email { get; set; }
}
}