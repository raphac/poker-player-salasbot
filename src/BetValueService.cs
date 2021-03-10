using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class BetValueService : IBetValueService
    {
        public int GetBetValue(Root root, int shouldBetValue)
        {
            // Make a call.
            if (shouldBetValue == 0)
            {
                return GetTurn(root, CardValuationType.Unplayable);
            }

            if (shouldBetValue <= 20)
            {
                return GetTurn(root, CardValuationType.NotRecommended);
            }
            
            if (shouldBetValue <= 75)
            {
                return GetTurn(root, CardValuationType.Risky);
            }
            
            return GetTurn(root, CardValuationType.Recommended);
        }
        
        public int GetTurn(Root root, CardValuationType playHand)
        {
            if (playHand == CardValuationType.Unplayable)
            {
                return 0;
            }
    
            if (playHand == CardValuationType.Risky)
            {
                var factor = RandomProvider.Next(5, 10);
                return Raise(root.MinimumRaise, factor);
            }
    
            if (playHand == CardValuationType.Recommended)
            {
                var factor = RandomProvider.Next(10, 20);
                return Raise(root.MinimumRaise, factor);
            }
    
            return CheckOrCall(root);
        }

        private int CheckOrCall(Root root)
        {
            var current_player = root.Players[root.InAction];
            var nextBet = root.CurrentBuyIn - current_player.Bet;

            return nextBet;
        }

        private int Raise(int minRaise, int factor)
        {
            return minRaise * factor;
        }
    }
}