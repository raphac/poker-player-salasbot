using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
			try
			{
				Root root = gameState.ToObject<Root>(); 
				var cardAnalyserService = new CardAnalyzerService();
				var betValueService = new BetValueService();
				//TODO: Use this method to return the value You want to bet
				if (cardAnalyserService.ShouldBet(root.players[root.in_action].hole_cards, root.community_cards))
				{
					return betValueService.GetBetValue(root);
				}

				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 12;
			}
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
	
	public interface IBetValueService
	{
		int GetBetValue(Root root);
	}
	
	public class BetValueService : IBetValueService
	{
		public int GetBetValue(Root root)
		{
			// Make a call.
			var current_player = root.Players[root.InAction];
			return root.CurrentBuyIn - current_player.Bet;
		}
	}

	public interface ICardAnalyzerService
	{
		bool ShouldBet(List<Card> cards);
	}

	public class CardAnalyzerService : ICardAnalyzerService
	{
		public bool ShouldBet(List<Card> cards)
		{
			return true;
		}
	}

    public partial class Root
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

    public partial class Card
    {
        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("suit")]
        public string Suit { get; set; }
    }

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

