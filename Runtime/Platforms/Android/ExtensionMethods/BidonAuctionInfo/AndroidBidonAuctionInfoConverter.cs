#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAuctionInfoConverter
    {
        private static readonly AndroidJavaClass AuctionInfoJClass;

        static AndroidBidonAuctionInfoConverter()
        {
            AuctionInfoJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.AuctionInfo");
        }

        public static BidonAuctionInfo ToBidonAuctionInfo(this AndroidJavaObject info)
        {
            if (!info.IsValidInstanceOf(AuctionInfoJClass)) return null;

            return new BidonAuctionInfo
            {
                AuctionId = info.SafeCall<string>("getAuctionId"),
                AuctionConfigurationId = info.SafeCall<AndroidJavaObject>("getAuctionConfigurationId")?.SafeCall<long>("longValue"),
                AuctionConfigurationUid = info.SafeCall<string>("getAuctionConfigurationUid"),
                AuctionTimeout = info.SafeCall<long>("getAuctionTimeout"),
                AuctionPriceFloor = info.SafeCall<double>("getAuctionPricefloor"),
                NoBids = info.SafeCall<AndroidJavaObject>("getNoBids").ToBidonAdUnitInfoList(),
                AdUnits = info.SafeCall<AndroidJavaObject>("getAdUnits").ToBidonAdUnitInfoList(),
            };
        }
    }
}
#endif
