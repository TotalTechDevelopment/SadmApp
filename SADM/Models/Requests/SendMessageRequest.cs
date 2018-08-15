namespace SADM.Models.Requests
{
    public class SendMessageRequest : RequestBase
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Drafting { get; set; }
    }
}
