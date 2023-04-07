#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAd
    {
        public string AdUnitId;
        public string AuctionId;
        public string CurrencyCode;
        public int AdType;
        public string Dsp;
        public double Ecpm;
        public string NetworkName;
        public string RoundId;

        public BidonAd ToBidonAd()
        {
            return new BidonAd
            {
                AdUnitId = AdUnitId,
                AuctionId = AuctionId,
                CurrencyCode = CurrencyCode,
                AdType = (BidonAdType)AdType,
                Dsp = Dsp,
                Ecpm = Ecpm,
                NetworkName = NetworkName,
                RoundId = RoundId,
            };
        }
    }
}
#endif
