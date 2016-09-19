using EloBuddy.SDK.Events;

namespace UBTaliyah
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
