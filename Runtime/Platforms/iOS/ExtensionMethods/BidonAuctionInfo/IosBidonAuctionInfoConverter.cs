#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal static class IosBidonAuctionInfoConverter
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginHelperFreeAuctionInfo")]
        private static extern void BidonAuctionInfoFree(IntPtr ptr);

        private static void FreeAuctionInfo(IntPtr ptr) => BidonAuctionInfoFree(ptr);

        public static BidonAuctionInfo ToBidonAuctionInfo(this IntPtr ptr)
        {
            var iosBidonAuctionInfo = IosBidonMarshal.SafePtrToStructure<IosBidonAuctionInfo>(ptr);
            var bidonAuctionInfo = iosBidonAuctionInfo?.ToBidonAuctionInfo();
            FreeAuctionInfo(ptr);
            return bidonAuctionInfo;
        }

        private static BidonAuctionInfo ToBidonAuctionInfo(this IosBidonAuctionInfo iosBidonAuctionInfo)
        {
            return new BidonAuctionInfo
            {
                AuctionId = iosBidonAuctionInfo.AuctionId,
                AuctionConfigurationId = iosBidonAuctionInfo.AuctionConfigurationId.ToNullableLong(),
                AuctionConfigurationUid = iosBidonAuctionInfo.AuctionConfigurationUid,
                AuctionTimeout = iosBidonAuctionInfo.AuctionTimeout,
                AuctionPriceFloor = iosBidonAuctionInfo.AuctionPriceFloor,
                NoBids = iosBidonAuctionInfo.NoBidsPtr.ToBidonAdUnitInfoList(),
                AdUnits = iosBidonAuctionInfo.AdUnitsPtr.ToBidonAdUnitInfoList(),
            };
        }
    }
}
#endif
