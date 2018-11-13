using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pixela
{
    internal class PixelaClientHandler : HttpClientHandler
    {
        private readonly PixelaClient _client;

        public PixelaClientHandler(PixelaClient client)
        {
            _client = client;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_client.AccessToken))
                request.Headers.Add("X-USER-TOKEN", _client.AccessToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}