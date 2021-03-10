using System.Linq;
using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class BetValueService : IBetValueService
    {
        public int GetBetValue(Root root, int shouldBetValue)
        {
            if (!root.CommunityCards.Any())
            {
                if (shouldBetValue > 18)
                {
                    return root.CurrentBuyIn - root.Players[root.InAction].Bet;
                }
            }
            
            if (shouldBetValue > 45)
            {
                return 8000;
            }

            return 0;
        }
    }
}