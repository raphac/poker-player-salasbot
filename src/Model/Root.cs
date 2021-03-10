using Newtonsoft.Json;

namespace Nancy.Simple.Model
{
    public class Root
    {
        [JsonProperty("tournament_id")]
        public string TournamentId { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("round")]
        public int Round { get; set; }

        [JsonProperty("bet_index")]
        public int BetIndex { get; set; }

        [JsonProperty("small_blind")]
        public int SmallBlind { get; set; }

        [JsonProperty("current_buy_in")]
        public int CurrentBuyIn { get; set; }

        [JsonProperty("pot")]
        public int Pot { get; set; }

        [JsonProperty("minimum_raise")]
        public int MinimumRaise { get; set; }

        [JsonProperty("dealer")]
        public int Dealer { get; set; }

        [JsonProperty("orbits")]
        public int Orbits { get; set; }

        [JsonProperty("in_action")]
        public int InAction { get; set; }

        [JsonProperty("players")]
        public Player[] Players { get; set; }

        [JsonProperty("community_cards")]
        public Card[] CommunityCards { get; set; }
    }
}