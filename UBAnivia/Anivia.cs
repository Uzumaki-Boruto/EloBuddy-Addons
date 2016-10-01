using EloBuddy.SDK.Events;

namespace UBAnivia
{
    class Anivia
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
