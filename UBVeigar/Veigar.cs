using EloBuddy.SDK.Events;

namespace UBVeigar
{
    class Veigar
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
