using Newtonsoft.Json;

namespace Pixela.Models
{
    public class GraphStats : ApiResponse
    {
        [JsonProperty("totalPixelsCount")]
        public long TotalPixelsCount { get; set; }

        [JsonProperty("maxQuantity")]
        public long MaxQuantity { get; set; }

        [JsonProperty("minQuantity")]
        public long MinQuantity { get; set; }

        [JsonProperty("totalQuantity")]
        public long TotalQuantity { get; set; }

        [JsonProperty("avgQuantity")]
        public long AvgQuantity { get; set; }

        [JsonProperty("todaysQuantity")]
        public long TodaysQuantity { get; set; }
    }
}