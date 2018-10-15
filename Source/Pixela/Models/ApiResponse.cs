using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pixela.Models
{
    public class ApiResponse
    {
        // This property should be null.
        [JsonExtensionData]
        public IDictionary<string, JToken> Extends { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}