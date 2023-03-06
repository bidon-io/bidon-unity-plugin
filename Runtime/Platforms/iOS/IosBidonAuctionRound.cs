#if UNITY_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAuctionRound
    {
        public string RoundId;
        public string Demands;
        public double Timeout;

        public BidonAd ToBidonAd()
        {
            return new BidonAd();
        }
    }
}
#endif
