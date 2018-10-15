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

        /// <summary>
        ///     Create a new Pixela user.
        /// </summary>
        /// <param name="token">authenticate token</param>
        /// <param name="username">username</param>
        /// <param name="agreeTermsOfService">agree to the terms of service</param>
        /// <param name="notMinor">you are not minor or have the parental consent of using this service</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Updates the authentication token for the specified user.
        /// </summary>
        /// <param name="token">new authenticate token</param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateAsync(string token)
        {
            var parameters = new Dictionary<string, object> {["newToken"] = token};
            var response = await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}", parameters).Stay();

            Client.AccessToken = token;

            return response;
        }

        /// <summary>
        ///     Delete the specified registered user.
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> DestroyAsync()
        {
            var response = await Client.SendAsync<ApiResponse>(HttpMethod.Delete, $"/v1/users/{Client.Username}").Stay();
            Client.Username = null;
            Client.AccessToken = null;

            return response;
        }
    }
}