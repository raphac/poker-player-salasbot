using Newtonsoft.Json;

namespace Nancy.Simple.Model
{
    public partial class Player
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("stack")]
        public int Stack { get; set; }

        [JsonProperty("bet")]
        public int Bet { get; set; }

        [JsonProperty("hole_cards", NullValueHandling = NullValueHandling.Ignore)]
        public Card[] HoleCards { get; set; }
    }
}