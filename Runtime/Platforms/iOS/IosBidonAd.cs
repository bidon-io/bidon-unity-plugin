#if UNITY_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAd
    {
        public string Id;
        public double Ecpm;
        public string AdUnitId;
        public string NetworkName;
        public string Dsp;

        public BidonAd ToBidonAd()
        {
            return new BidonAd
            {
                AdUnitId = AdUnitId,
                AuctionId = Id,
                CurrencyCode = "USD",
                DemandAd = null,
                Dsp = Dsp,
                Ecpm = Ecpm,
                NetworkName = NetworkName,
                RoundId = "",
            };
        }
    }
}
#endif
