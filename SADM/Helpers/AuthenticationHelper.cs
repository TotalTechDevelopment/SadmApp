using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using SADM.Models.Requests;
using SADM.Models.Responses;
using SADM.Services;

namespace SADM.Helpers
{
	public class AuthenticationHelper : HttpClientHandler
    {
        protected ISettingsService settingsService;
        protected ISadmApiService sadmApiService;

        protected string token;
        protected DateTime expireDateTime = DateTime.Now;

        public AuthenticationHelper(ISadmApiService sadmApiService, ISettingsService settingsService)
        {
            this.sadmApiService = sadmApiService;
            this.settingsService = settingsService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, await GetToken());
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        protected async Task<string> GetToken()
        {
            if(token == null || DateTime.Now >= expireDateTime)
            {
                var response = await sadmApiService.CallServiceAsync<GetAppTokenRequest, GetAppTokenResponse>(new GetAppTokenRequest());
                expireDateTime = DateTime.Now.Add(TimeSpan.FromSeconds(response.ExpiresIn));
                token = response.Token;
            }
            if(token is null)
            {
                throw new ApiTokenException();
            }
            return token;
        }
    }
}
