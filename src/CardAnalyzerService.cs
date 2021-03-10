using System.Collections.Generic;
using System.Linq;
using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class CardAnalyzerService : ICardAnalyzerService
    {
        public int ShouldBet(Card[] handCards, Card[] communityCards)
        {
            var ownFirstCard = handCards.First();
            var ownSecondCard = handCards.Last();
			
            var cardValues = new int[]
            {
                PairValue(handCards, communityCards),
                TwoPairValue(handCards, communityCards),
                ThreeOfKindValue(handCards, communityCards),
                FourOfKindValue(handCards, communityCards),
                FlushValue(handCards, communityCards),
                FullHouseValue(handCards, communityCards),
                StraightValue(handCards, communityCards),
                StraightFlushValue(handCards, communityCards),
                communityCards.Any() ? 0 : StartHandValue(ownFirstCard, ownSecondCard)
            };

            var cardValue = cardValues.Max();

            if (IsBadHand(ownFirstCard, ownSecondCard))
            {
                cardValue = 0;
            }

            return cardValue;
        }

        private bool IsBadHand(Card ownFirstCard, Card ownSecondCard)
        {
            return (RankValueByRank[ownFirstCard.Rank] == 2 && RankValueByRank[ownSecondCard.Rank] == 7) ||
                     (RankValueByRank[ownFirstCard.Rank] == 7 && RankValueByRank[ownSecondCard.Rank] == 2);
        }

        private int StartHandValue(Card first, Card second)
        {
            var firstRank = RankValueByRank[first.Rank];
            var secondRank = RankValueByRank[second.Rank];
            if (firstRank >= 8 || secondRank >= 8)
            {
                return firstRank + secondRank;
            }

            return 0;
        }

        private int PairValue(Card[] handCards, Card[] communityCards)
        {
            var pair = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .SingleOrDefault(g => g.Count() == 2);
            if (pair != null)
            {
                var communitCardsValue = communityCards
                    .GroupBy(card => RankValueByRank[card.Rank]).Count(g => Enumerable.Count<Card>(g) == 2) > 0;

                if (communitCardsValue)
                {
                    return pair.Key / 2;
                }
                
                return pair.Key;
            }
            else
            {
                return 0;
            }
        }
        
        private int TwoPairValue(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 2)
                .Select(y => y.Key)
                .ToList();
            
            var value = duplicates.Count == 2 ? 2 * duplicates.Max() : 0;

            var communityCardDuplicates = communityCards.GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 2)
                .Select(y => y.Key)
                .ToList();
            
            var communityCardValue = communityCardDuplicates.Count == 2 ? 2 * duplicates.Max() : 0;
            
            return communityCardValue > 0 ? value / 2 : value;
        }
        
        private int ThreeOfKindValue(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 3)
                .Select(y => y.Key)
                .ToList();
            
            var value = duplicates.Count > 0 ? 3 * duplicates.Max() : 0;
            
            var communityDuplicates = communityCards.GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 3)
                .Select(y => y.Key)
                .ToList();
            
            var communityValue = communityDuplicates.Count > 0 ? 3 * duplicates.Max() : 0;

            return communityValue > 0 ? value / 2 : value;
        }
        
        private int StraightValue(Card[] handCards, Card[] communityCards)
        {
            var cards = handCards.Union(communityCards).Select(card => RankValueByRank[card.Rank]).OrderByDescending(x => x);

            var count = 0;
            var currentRank = 0;
            foreach (var card in cards)
            {
                if (count == 0)
                {
                    count = 1;
                    currentRank = card;
                    continue;
                }

                if (currentRank - 1 == card)
                {
                    count++;
                }
                else
                {
                    count = 1;
                }

                currentRank = card;
                
                if (count == 5)
                {
                    return currentRank * 4;
                }
            }

            return 0;
        }
        
        private int FlushValue(Card[] handCards, Card[] communityCards)
        {
            var cards = handCards.Union(communityCards).ToList();
            var duplicates = cards.GroupBy(card => card.Suit)
                .Where(g => g.Count() >= 5)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count > 0 ? 8 * cards.Max(c => RankValueByRank[c.Rank]) : 0;
        }
        
        private int FullHouseValue(Card[] handCards, Card[] communityCards)
        {
            var intermediateValue = ThreeOfKindValue(handCards, communityCards) * PairValue(handCards, communityCards);
            return intermediateValue > 0 ? 13 * ThreeOfKindValue(handCards, communityCards) / 3 : intermediateValue;
        }
        
        private int FourOfKindValue(Card[] handCards, Card[] communityCards)
        {
            var duplicates = handCards.Union(communityCards).GroupBy(card => RankValueByRank[card.Rank])
                .Where(g => g.Count() == 4)
                .Select(y => y.Key)
                .ToList();
            return duplicates.Count > 0 ? 5 * duplicates.Max() : 0;
        }

        private int StraightFlushValue(Card[] handCards, Card[] communityCards)
        {
            var intermediateValue = FlushValue(handCards, communityCards) * StraightValue(handCards, communityCards);
            return intermediateValue > 0 ? 6 * FlushValue(handCards, communityCards) / 8 : intermediateValue;
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