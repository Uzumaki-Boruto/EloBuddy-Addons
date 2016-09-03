using EloBuddy.SDK.Events;

namespace UBAzir
{
    class Azir
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
