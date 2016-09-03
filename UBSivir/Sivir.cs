using EloBuddy.SDK.Events;

namespace UBSivir
{
    class Sivir
    {
        static void Main(string[] args)
        {
            {
                Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
            }
        }
    }
}
