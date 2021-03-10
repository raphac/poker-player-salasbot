using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public class CardAnalyzerService : ICardAnalyzerService
    {
        public bool ShouldBet(List<Card> handCards, List<Card> communityCards)
        {
            var ownFirstCard = handCards.First();
            var ownSecondCard = handCards.Last();
			
            var shouldBet = RankValueByRank[ownFirstCard.rank] == RankValueByRank[ownSecondCard.rank];
            shouldBet = ownFirstCard.suit == ownSecondCard.suit;
			
            return shouldBet;
        }
		
        private static IDictionary<string, int> RankValueByRank = new Dictionary<string, int>
        {
            {"A", 14},
            {"K", 13},
            {"Q", 12},
            {"J", 11},
            {"10", 10},
            {"9", 9},
            {"8", 8},
            {"7", 7},
            {"6", 6},
            {"5", 5},
            {"4", 4},
            {"3", 3},
            {"2", 2}
        };
    }
}