using Newtonsoft.Json;

namespace Nancy.Simple.Model
{
    public partial class Card
    {
        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("suit")]
        public string Suit { get; set; }
    }
}