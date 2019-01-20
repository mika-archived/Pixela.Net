using Newtonsoft.Json;

namespace Pixela.Models
{
    public class Pixel
    {
        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("optionalData")]
        public string OptionalData { get; set; }
    }
}