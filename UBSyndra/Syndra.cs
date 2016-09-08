using EloBuddy.SDK.Events;

namespace UBSyndra
{
    class Syndra
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
