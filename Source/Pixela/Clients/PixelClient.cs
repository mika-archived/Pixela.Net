using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Pixela.Extensions;
using Pixela.Models;

namespace Pixela.Clients
{
    public class PixelClient : ApiClient
    {
        internal PixelClient(PixelaClient client) : base(client) { }

        private async Task<ApiResponse> CreateAsyncInternal<T>(string graphId, DateTime date, T quantity)
        {
            var parameters = new Dictionary<string, object>
            {
                ["date"] = date.ToString("yyyyMMdd"),
                ["quantity"] = quantity.ToString()
            };

            return await Client.SendAsync<ApiResponse>(HttpMethod.Post, $"/v1/users/{Client.Username}/graphs/{graphId}", parameters).Stay();
        }

        public async Task<ApiResponse> CreateAsync(string graphId, DateTime date, float quantity)
        {
            return await CreateAsyncInternal(graphId, date, quantity).Stay();
        }

        public async Task<ApiResponse> CreateAsync(string graphId, DateTime date, int quantity)
        {
            return await CreateAsyncInternal(graphId, date, quantity).Stay();
        }

        public async Task<object> ShowAsync(string graphId, DateTime date)
        {
            var response = await Client.GetAsync<ApiResponse>($"/v1/users/{Client.Username}/graphs/{graphId}/{date:yyyyMMdd}").Stay();
            return response.Extends["quantity"];
        }

        private async Task<ApiResponse> UpdateAsyncInternal<T>(string graphId, DateTime date, T quantity)
        {
            var parameters = new Dictionary<string, object> {["quantity"] = quantity.ToString()};
            return await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}/graphs/{graphId}/{date:yyyyMMdd}", parameters).Stay();
        }

        public async Task<ApiResponse> UpdateAsync(string graphId, DateTime date, float quantity)
        {
            return await UpdateAsyncInternal(graphId, date, quantity).Stay();
        }

        public async Task<ApiResponse> UpdateAsync(string graphId, DateTime date, int quantity)
        {
            return await UpdateAsyncInternal(graphId, date, quantity).Stay();
        }

        public async Task<ApiResponse> IncrementAsync(string graphId)
        {
            return await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}/graphs/{graphId}/increment").Stay();
        }

        public async Task<ApiResponse> DecrementAsync(string graphId)
        {
            return await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}/graphs/{graphId}/decrement").Stay();
        }

        public async Task<ApiResponse> DestroyAsync(string graphId, DateTime date)
        {
            return await Client.SendAsync<ApiResponse>(HttpMethod.Delete, $"/v1/users/{Client.Username}/graphs/{graphId}/{date:yyyyMMdd}").Stay();
        }
    }
}