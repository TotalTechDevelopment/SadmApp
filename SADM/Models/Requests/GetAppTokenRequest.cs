namespace SADM.Models.Requests
{
    public class GetAppTokenRequest : RequestBase
    {
        [Refit.AliasAs("grant_type")]
        public string GrantType => SADM.Settings.AppConfiguration.Values.OauthGrantType;
        [Refit.AliasAs("username")]
        public string Username => SADM.Settings.AppConfiguration.Values.OauthUserName;
        [Refit.AliasAs("password")]
        public string Password => SADM.Settings.AppConfiguration.Values.OauthPassword;
    }
}