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
				//TODO: Use this method to return the value You want to bet
				var shouldBet = cardAnalyserService.ShouldBet(root.Players[root.InAction].HoleCards, root.CommunityCards);
				if (shouldBet
				    || root.Round % 2 == 0)
				{
					return betValueService.GetBetValue(root, 10, shouldBet); // TODO
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
}

