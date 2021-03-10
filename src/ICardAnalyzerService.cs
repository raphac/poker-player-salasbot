using System.Collections.Generic;
using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public interface ICardAnalyzerService
    {
        bool ShouldBet(Card[] handCards, Card[] communityCards);
    }
}