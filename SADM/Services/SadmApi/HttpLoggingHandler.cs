using System.Net.Http;

namespace SADM.Services.SadmApi
{
    internal class HttpLoggingHandler : HttpMessageHandler
    {
        private object getToken;

        public HttpLoggingHandler(object getToken)
        {
            this.getToken = getToken;
        }
    }
}