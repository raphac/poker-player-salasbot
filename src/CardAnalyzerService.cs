using System.Collections.Generic;
using System.Linq;
using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class CardAnalyzerService : ICardAnalyzerService
    {
        public bool ShouldBet(Card[] handCards, Card[] communityCards)
        {
            var ownFirstCard = handCards.First();
            var ownSecondCard = handCards.Last();
			
            var shouldBet = RankValueByRank[ownFirstCard.Rank] == RankValueByRank[ownSecondCard.Rank] ||
                            handCards.All(card => RankValueByRank[card.Rank] >= 10) ||
                            !((RankValueByRank[ownFirstCard.Rank] == 2 && RankValueByRank[ownFirstCard.Rank] == 7) ||
                              (RankValueByRank[ownFirstCard.Rank] == 7 && RankValueByRank[ownFirstCard.Rank] == 2));
            
            return shouldBet;
        }

        private static bool IsFourOfAKind(Card[] handCards, Card[] communityCards)
        {
            var countMap = new Dictionary<int, int>();
            
            // Initialize map with a counter of 0 for each rank.
            foreach (var value in RankValueByRank.Values)
            {
                countMap[value] = 0;
            }
            
            // Count all the ranks.
            foreach (var card in handCards.Concat(communityCards))
            {
                int key = RankValueByRank[card.Rank];
                countMap[key]++;
            }
            
            // Check if we have a "four-of-a-kind".
            return countMap.Values.Any(value => value == 4);
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