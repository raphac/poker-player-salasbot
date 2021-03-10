using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class BetValueService : IBetValueService
    {
        public int GetBetValue(Root root, int goodCardsIndex)
        {
            // Make a call.
            var current_player = root.Players[root.InAction];
            if (root.CurrentBuyIn > 900)
            {
                return 0;
            }
            
            return root.CurrentBuyIn - current_player.Bet;
        }
    }
}