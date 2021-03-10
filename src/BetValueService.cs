using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class BetValueService : IBetValueService
    {
        public int GetBetValue(Root root, int goodCardsIndex)
        {
            // Make a call.
            var current_player = root.Players[root.InAction];
            var nextBet = root.CurrentBuyIn - current_player.Bet;
            if (root.CurrentBuyIn > 900 && current_player.Bet < 50)
            {
                return 0;
            }

            return nextBet;
        }
        
        // public int GetTurn(Root root, CardValuationType playHand)
        // {
        //     // if (context.RoundType == GameRoundType.PreFlop)
        //     // {
        //         var currentPlayer = root.Players[root.InAction];
        //         if (playHand == CardValuationType.Unplayable)
        //         {
        //             return 0;
        //         }
        //
        //         var isRaiseOptionAvailable = CanRaise();
        //         if (playHand == CardValuationType.Risky && isRaiseOptionAvailable)
        //         {
        //             var factor = RandomProvider.Next(1, 4);
        //             return RaiseOrAllIn(
        //                 root.MinimumRaise, root.context.CurrentMaxBet, currentPlayer.Stack, context.MoneyToCall, factor);
        //         }
        //
        //         if (playHand == CardValuationType.Recommended && isRaiseOptionAvailable)
        //         {
        //             var factor = RandomProvider.Next(3, 6);
        //             return RaiseOrAllIn(
        //                 context.MinRaise, context.CurrentMaxBet, context.MoneyLeft, context.MoneyToCall, factor);
        //         }
        //
        //         return CheckOrCall(root);
        //     // }
        //     //
        //     // return PlayerAction.CheckOrCall();
        // }

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

        private bool CanRaise()
        {
            return true; // TODO
        }

        private int RaiseOrAllIn(int minRaise, int currentMaxBet, int moneyLeft, int moneyToCall, int factor)
        {
            if ((minRaise * factor) + currentMaxBet > moneyLeft)
            {
                // All-in
                return moneyLeft - moneyToCall;
            }
            else
            {
                return minRaise * factor;
            }
        }
    }
}