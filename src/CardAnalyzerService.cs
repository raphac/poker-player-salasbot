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
			
            var shouldBet = IsPair(handCards, communityCards) ||
                            IsTwoPair(handCards, communityCards) ||
                            IsThreeOfKind(handCards, communityCards) ||
                            IsFourOfKind(handCards, communityCards) ||
                            IsFlush(handCards, communityCards) ||
                            IsFullHouse(handCards, communityCards) ||
                            handCards.All(card => RankValueByRank[card.Rank] >= 10) &&
                            !IsBadHand(ownFirstCard);

            return shouldBet;
        }

        private static bool IsBadHand(Card ownFirstCard)
        {
            return (RankValueByRank[ownFirstCard.Rank] == 2 && RankValueByRank[ownFirstCard.Rank] == 7) ||
                     (RankValueByRank[ownFirstCard.Rank] == 7 && RankValueByRank[ownFirstCard.Rank] == 2);
        }

        private bool IsPair(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 2)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count == 1;
        }
        
        private bool IsTwoPair(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 2)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count == 2;
        }
        
        private bool IsThreeOfKind(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 3)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count > 0;
        }
        
        private bool IsFourOfKind(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 4)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count > 0;
        }
        
        private bool IsFullHouse(Card[] handCards, Card[] communityCards)
        {
            return IsThreeOfKind(handCards, communityCards) && IsPair(handCards, communityCards);
        }

        // private bool IsStraight(Card[] handCards, Card[] communityCards)
        // {
        //     
        // }
        
        private bool IsFlush(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => card.Suit)
                .Where(g => g.Count() >= 5)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count > 0;
        }
        
        // private bool IsStraightFlush(Card[] handCards, Card[] communityCards)
        // {
        //     return IsFlush(handCards, communityCards) && IsStraight(handCards, communityCards);
        // }
		
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