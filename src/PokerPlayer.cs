using System;
using Nancy.Simple.Model;
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
				
				var shouldBetValue = cardAnalyserService.ShouldBet(root.Players[root.InAction].HoleCards, root.CommunityCards);

				return betValueService.GetBetValue(root, shouldBetValue);
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
}

