using Nancy.Simple.Model;

namespace Nancy.Simple
{
    public interface IBetValueService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="goodCardsIndex">0 - 10 and 10 is very good</param>
        /// <returns></returns>
        int GetBetValue(Root root, int goodCardsIndex);
    }
}