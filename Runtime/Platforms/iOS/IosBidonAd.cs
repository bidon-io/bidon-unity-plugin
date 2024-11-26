#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAd
    {
        [MarshalAs(UnmanagedType.Struct)]
        public IosBidonAdUnit AdUnit;
        public string AuctionId;
        public string CurrencyCode;
        public int AdType;
        public double Ecpm;
        public string NetworkName;

        public BidonAd ToBidonAd()
        {
            return new BidonAd
            {
                AdUnit = AdUnit.ToBidonAdUnit(),
                AuctionId = AuctionId,
                CurrencyCode = CurrencyCode,
                AdType = (BidonAdType)AdType,
                BidType = BidonBidType.Cpm, // TODO: replace with actual value / remove after sync between native platforms
                Dsp = "", // TODO: replace with actual value / remove after sync between native platforms
                Ecpm = Ecpm,
                NetworkName = NetworkName,
            };
        }
    }
}
#endif
