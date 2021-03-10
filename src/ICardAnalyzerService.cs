using System.Collections.Generic;
using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public interface ICardAnalyzerService
    {
        int ShouldBet(Card[] handCards, Card[] communityCards);
    }
}