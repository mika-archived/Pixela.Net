using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Pixela.Extensions;
using Pixela.Models;

namespace Pixela.Clients
{
    public class UsersClient : ApiClient
    {
        internal UsersClient(PixelaClient client) : base(client) { }

        public async Task<ApiResponse> CreateAsync(string token, string username, bool agreeTermsOfService, bool notMinor)
        {
            var parameters = new Dictionary<string, object>
            {
                ["token"] = token,
                ["username"] = username,
                ["agreeTermsOfService"] = agreeTermsOfService ? "yes" : "no",
                ["notMinor"] = notMinor ? "yes" : "no"
            };

            Client.AccessToken = token;
            Client.Username = username;

            return await Client.SendAsync<ApiResponse>(HttpMethod.Post, "/v1/users", parameters).Stay();
        }

        public async Task<ApiResponse> UpdateAsync(string token)
        {
            var parameters = new Dictionary<string, object> {["newToken"] = token};
            var response = await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}", parameters).Stay();

            Client.AccessToken = token;

            return response;
        }

        public async Task<ApiResponse> DestroyAsync()
        {
            var response = await Client.SendAsync<ApiResponse>(HttpMethod.Delete, $"/v1/users/{Client.Username}").Stay();
            Client.Username = null;
            Client.AccessToken = null;

            return response;
        }
    }
}