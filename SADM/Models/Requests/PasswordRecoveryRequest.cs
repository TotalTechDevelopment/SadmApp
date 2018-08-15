namespace SADM.Models.Requests
{
    public class PasswordRecoveryRequest: RequestBase
    {
        public string Email { get; set; }
    }
}
