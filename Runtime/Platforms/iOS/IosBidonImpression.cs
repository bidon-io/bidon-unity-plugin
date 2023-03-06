#if UNITY_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonImpression
    {
        public string ImpressionId;
        public string AuctionId;
        public int AuctionConfigurationId;
        public IosBidonAd Ad;
        public double ShowTrackTime;
        public double ClickTrackTime;
        public double RewardTrackTime;

        public BidonAd ToBidonAd()
        {
            return new BidonAd();
        }
    }
}
#endif
