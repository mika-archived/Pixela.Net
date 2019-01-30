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

        /// <summary>
        ///     Create a new pixelation graph definition.
        /// </summary>
        /// <param name="id">ID for identifying the pixelation graph, format ^[a-z][a-z0-9-]{1, 16}$</param>
        /// <param name="name">name of the pixelation graph.</param>
        /// <param name="unit">unit of the quantity</param>
        /// <param name="type">type of quantity</param>
        /// <param name="color">display color of the pixel</param>
        /// <param name="timezone">timezone for handling this graph, default UTC</param>
        /// <param name="selfSufficient">when SVG graph referenced, pixel of this graph itself will be increment or decrement, default none</param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateAsync(string id, string name, string unit, GraphType type, GraphColor color, string timezone = null, SufficientType? selfSufficient = null)
        {
            var parameters = new Dictionary<string, object>
            {
                ["id"] = id,
                ["name"] = name,
                ["unit"] = unit,
                ["type"] = type.AsString(),
                ["color"] = color.AsString()
            };
            if (!string.IsNullOrWhiteSpace(timezone))
                parameters.Add("timezone", timezone);
            if(selfSufficient.HasValue)
                parameters.Add("selfSufficient", selfSufficient.Value.AsString());

            return await Client.SendAsync<ApiResponse>(HttpMethod.Post, $"/v1/users/{Client.Username}/graphs", parameters).Stay();
        }

        /// <summary>
        ///     Get all predefined pixelation graph definitions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Graph>> ListAsync()
        {
            var response = await Client.GetAsync<ApiResponse>($"/v1/users/{Client.Username}/graphs").Stay();
            return JsonConvert.DeserializeObject<IEnumerable<Graph>>(response.Extends["graphs"].ToString());
        }

        /// <summary>
        ///     Based on the registered information, express the graph in SVG format diagram
        /// </summary>
        /// <param name="graphId">ID of graph</param>
        /// <param name="date">If specify date, will create a graph dating back to the past with that day as the start date</param>
        /// <param name="mode">Graph display mode</param>
        /// <returns></returns>
        public async Task<Stream> ShowAsync(string graphId, DateTime? date, string mode = null)
        {
            var parameters = new Dictionary<string, object>();
            if (date.HasValue)
                parameters.Add("date", date.Value.ToString("yyyyMMdd"));
            if (!string.IsNullOrWhiteSpace(mode))
                parameters.Add("mode", mode);

            return await Client.GetAsStreamAsync($"/v1/users/{Client.Username}/graphs/{graphId}", parameters).Stay();
        }

        /// <summary>
        ///     Update predefined pixelation graph definitions.
        /// </summary>
        /// <param name="graphId">ID of graph</param>
        /// <param name="name">name of the pixelation graph</param>
        /// <param name="unit">unit of the quantity</param>
        /// <param name="color">display color of the pixel</param>
        /// <param name="timezone">timezone for handling this graph</param>
        /// <param name="purgeCacheUrls"></param>
        /// <param name="selfSufficient">when SVG graph referenced, pixel of this graph itself will be increment or decrement, default none</param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateAsync(string graphId, string name = null, string unit = null, GraphColor? color = null, string timezone = null, List<string> purgeCacheUrls = null, SufficientType? selfSufficient = null)
        {
            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(name))
                parameters.Add("name", name);
            if (!string.IsNullOrWhiteSpace(unit))
                parameters.Add("unit", unit);
            if (color.HasValue)
                parameters.Add("color", color.Value.AsString());
            if (!string.IsNullOrWhiteSpace(timezone))
                parameters.Add("timezone", timezone);
            if (purgeCacheUrls != null)
                parameters.Add("purgeCacheURLs", purgeCacheUrls);
            if (selfSufficient.HasValue)
                parameters.Add("selfSufficient", selfSufficient.Value.AsString());

            return await Client.SendAsync<ApiResponse>(HttpMethod.Put, $"/v1/users/{Client.Username}/graphs/{graphId}", parameters).Stay();
        }

        /// <summary>
        ///     Delete the predefined pixelation graph definition.
        /// </summary>
        /// <param name="graphId">ID of graph</param>
        /// <returns></returns>
        public async Task<ApiResponse> DestroyAsync(string graphId)
        {
            return await Client.SendAsync<ApiResponse>(HttpMethod.Delete, $"/v1/users/{Client.Username}/graphs/{graphId}").Stay();
        }

        /// <summary>
        ///     Displays the details of the graph in html format.
        /// </summary>
        /// <param name="graphId">UD of graph</param>
        /// <returns></returns>
        public async Task<string> DetailsAsync(string graphId)
        {
            return await Task.FromResult($"https://pixe.la/v1/users/{Client.Username}/graphs/{graphId}.html").Stay();
        }
    }
}