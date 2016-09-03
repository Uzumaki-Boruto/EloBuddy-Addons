using EloBuddy.SDK.Events;

namespace UBKennen
{
    class Kennen
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
