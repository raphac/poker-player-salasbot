using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
			var cardAnalyserService = new CardAnalyzerService();
			var betValueService = new BetValueService();
			//TODO: Use this method to return the value You want to bet
			if (cardAnalyserService.ShouldBet(null))
			{
				return betValueService.GetBetValue(null);
			}

			return 0;
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
			return 10;
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

	public class Player
	{
		public string name { get; set; }
		public int stack { get; set; }
		public string status { get; set; }
		public int bet { get; set; }
		public List<Card> hole_cards { get; set; }
		public string version { get; set; }
		public int id { get; set; }
	}

	public class Root
	{
		public List<Player> players { get; set; }
		public string tournament_id { get; set; }
		public string game_id { get; set; }
		public int round { get; set; }
		public int bet_index { get; set; }
		public int small_blind { get; set; }
		public int orbits { get; set; }
		public int dealer { get; set; }
		public List<Card> community_cards { get; set; }
		public int current_buy_in { get; set; }
		public int pot { get; set; }
	}

	public class Card
	{
		public int rank { get; set; }
		public string suit { get; set; }
	}
}

