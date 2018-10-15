using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Pixela.Enums;
using Pixela.Extensions;
using Pixela.Models;

namespace Pixela.Clients
{
    public class GraphsClient : ApiClient
    {
        internal GraphsClient(PixelaClient client) : base(client) { }

        public async Task<ApiResponse> CreateAsync(string id, string name, string unit, GraphType type, GraphColor color)
        {
            var parameters = new Dictionary<string, object>
            {
                ["id"] = id,
                ["name"] = name,
                ["unit"] = unit,
                ["type"] = type.AsString(),
                ["color"] = color.AsString()
            };

            return await Client.SendAsync<ApiResponse>(HttpMethod.Post, $"/v1/users/{Client.Username}/graphs", parameters).Stay();
        }

        public async Task<IEnumerable<Graph>> ListAsync()
        {
            var response = await Client.GetAsync<ApiResponse>($"/v1/users/{Client.Username}/graphs").Stay();
            return JsonConvert.DeserializeObject<IEnumerable<Graph>>(response.Extends["graphs"].ToString());
        }

        public async Task<Stream> ShowAsync(string graphId, DateTime? date)
        {
            var parameters = new Dictionary<string, object>();
            if (date.HasValue)
                parameters.Add("date", date.Value.ToString("yyyyMMdd"));

            return await Client.GetAsStreamAsync($"/v1/users/{Client.Username}/graphs/{graphId}", parameters).Stay();
        }

        public async Task<ApiResponse> UpdateAsync(string graphId, string name, string unit, GraphColor color)
        {
            var parameters = new Dictionary<string, object>
            {
                ["name"] = name,
                ["unit"] = unit,
                ["color"] = color.AsString()
            };

            return await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}/graphs/{graphId}", parameters).Stay();
        }

        public async Task<ApiResponse> DestroyAsync(string graphId)
        {
            return await Client.SendAsync<ApiResponse>(HttpMethod.Delete, $"/v1/users/{Client.Username}/graphs/{graphId}").Stay();
        }
    }
}