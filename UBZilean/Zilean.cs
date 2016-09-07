using EloBuddy.SDK.Events;

namespace UBZilean
{
    class Zilean
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
