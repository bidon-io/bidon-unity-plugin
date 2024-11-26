#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAuctionInfo
    {
        public string AuctionId;
        public string AuctionConfigurationId;
        public string AuctionConfigurationUid;
        public double AuctionPriceFloor;
        [MarshalAs(UnmanagedType.Struct)]
        public IosBidonAdUnitInfoArray NoBids;
        [MarshalAs(UnmanagedType.Struct)]
        public IosBidonAdUnitInfoArray AdUnits;
        public long Timeout;
        public string Description;

        public BidonAuctionInfo ToBidonAuctionInfo()
        {
            return new BidonAuctionInfo
            {
                AuctionId = AuctionId,
                AuctionConfigurationId = 0,
                AuctionConfigurationUid = AuctionConfigurationUid,
                AuctionTimeout = Timeout,
                AuctionPriceFloor = AuctionPriceFloor,
                NoBids = NoBids.ToBidonAdUnitInfoList(),
                AdUnits = AdUnits.ToBidonAdUnitInfoList(),
            };
        }
    }
}
#endif
