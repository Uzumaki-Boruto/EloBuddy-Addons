using EloBuddy.SDK.Events;

namespace UBRyze
{
    class Ryze
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
