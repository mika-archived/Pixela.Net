using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Pixela.Enums;

namespace Pixela.Models
{
    public class Graph
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GraphType Type { get; set; }

        [JsonProperty("color")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GraphColor Color { get; set; }
    }
}