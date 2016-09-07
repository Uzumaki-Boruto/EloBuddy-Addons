using EloBuddy.SDK.Events;

namespace UBLucian
{
    class Lucian
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
