using EloBuddy.SDK.Events;

namespace UBBard
{
    class Bard
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
