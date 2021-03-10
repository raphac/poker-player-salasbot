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

            if (shouldBetValue <= 50)
            {
                return GetTurn(root, CardValuationType.NotRecommended);
            }
            
            if (shouldBetValue <= 150)
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
    
            var isRaiseOptionAvailable = CanRaise(root);
            if (playHand == CardValuationType.Risky && isRaiseOptionAvailable)
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
            if (root.CurrentBuyIn > 900 && current_player.Bet < 50)
            {
                return 0;
            }

            return nextBet;
        }

        private bool CanRaise(Root root)
        {
            var currentPlayer = root.Players[root.InAction];
            var nextBet = root.CurrentBuyIn - currentPlayer.Bet;
            return nextBet <= currentPlayer.Stack;
        }

        private int Raise(int minRaise, int factor)
        {
            return minRaise * factor;
        }
    }
}