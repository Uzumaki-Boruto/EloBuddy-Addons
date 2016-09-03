using EloBuddy.SDK.Events;

namespace UBNidalee
{
    class Nidalee
    {
        static void Main(string[] args)
        {
            {
                Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
            }
        }
    }
}
