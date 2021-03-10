using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public class BetValueService : IBetValueService
    {
        public int GetBetValue(Root root, int shouldBetValue)
        {
            if (shouldBetValue > 45)
            {
                return 8000;
            }

            return 0;
        }
    }
}