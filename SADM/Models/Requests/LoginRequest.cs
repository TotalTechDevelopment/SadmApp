namespace SADM.Models.Requests
{
    public class LoginRequest : RequestBase
    {
        [Refit.AliasAs("email")]
        public string Email { get; set; }
        [Refit.AliasAs("password")]
        public string Password { get; set; }
    }
}