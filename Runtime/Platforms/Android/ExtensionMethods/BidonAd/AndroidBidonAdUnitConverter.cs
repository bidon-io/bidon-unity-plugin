#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdUnitConverter
    {
        private static readonly AndroidJavaClass AdUnitJClass;

        static AndroidBidonAdUnitConverter()
        {
            AdUnitJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.auction.models.AdUnit");
        }

        public static BidonAdUnit ToBidonAdUnit(this AndroidJavaObject adUnit)
        {
            if (!adUnit.IsValidInstanceOf(AdUnitJClass)) return null;

            return new BidonAdUnit
            {
                DemandId = adUnit.SafeCall<string>("getDemandId"),
                Label = adUnit.SafeCall<string>("getLabel"),
                PriceFloor = adUnit.SafeCall<double>("getPricefloor"),
                Uid = adUnit.SafeCall<string>("getUid"),
                BidType = adUnit.SafeCall<AndroidJavaObject>("getBidType").ToBidonBidType(),
                ExtJson = adUnit.SafeCall<AndroidJavaObject>("getExtra").SafeCall<string>("toString"),
            };
        }
    }
}
#endif
