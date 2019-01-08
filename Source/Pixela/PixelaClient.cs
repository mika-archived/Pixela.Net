using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Pixela.Clients;
using Pixela.Extensions;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Pixela
{
    public class PixelaClient
    {
        private readonly HttpClient _client;
        public static string Version => "1.0";

        public string AccessToken { get; set; }
        public string Username { get; set; }

        public GraphsClient Graphs { get; }
        public PixelClient Pixel { get; }
        public UsersClient Users { get; }

        public PixelaClient(string username = null, string accessToken = null)
        {
            Username = username;
            AccessToken = accessToken;

            _client = new HttpClient(new PixelaClientHandler(this));
            _client.DefaultRequestHeaders.Add("User-Agent", $"PixelaClient.NET/{Version}");

            Graphs = new GraphsClient(this);
            Pixel = new PixelClient(this);
            Users = new UsersClient(this);
        }

        internal async Task<T> GetAsync<T>(string endpoint, IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
                endpoint += $"?{string.Join("&", parameters.Select(w => $"{w.Key}=${w.Value}"))}";

            var response = await _client.GetAsync("https://pixe.la" + endpoint).Stay();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().Stay());
        }

        internal async Task<Stream> GetAsStreamAsync(string endpoint, IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
                endpoint += $"?{string.Join("&", parameters.Select(w => $"{w.Key}=${w.Value}"))}";

            var response = await _client.GetAsync("https://pixe.la" + endpoint).Stay();
            return await response.Content.ReadAsStreamAsync().Stay();
        }

        internal async Task<T> SendAsync<T>(HttpMethod method, string endpoint, IDictionary<string, object> parameters = null)
        {
            var url = "https://pixe.la" + endpoint;

            HttpResponseMessage response;
            if (parameters != null)
            {
                var json = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.SendAsync(new HttpRequestMessage(method, url) {Content = content}).Stay();
            }
            else
            {
                response = await _client.SendAsync(new HttpRequestMessage(method, url)).Stay();
            }

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().Stay());
        }
    }
}