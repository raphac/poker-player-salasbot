using System.Collections.Generic;

namespace Nancy.Simple
{
    public interface ICardAnalyzerService
    {
        bool ShouldBet(List<Card> handCards, List<Card> communityCards);
    }
}