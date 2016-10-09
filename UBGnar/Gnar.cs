using EloBuddy.SDK.Events;

namespace UBGnar
{
    class Gnar
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
