#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdUnitInfoConverter
    {
        private static readonly AndroidJavaClass AdUnitInfoJClass;

        static AndroidBidonAdUnitInfoConverter()
        {
            AdUnitInfoJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.AdUnitInfo");
        }

        public static BidonAdUnitInfo ToBidonAdUnitInfo(this AndroidJavaObject info)
        {
            if (!info.IsValidInstanceOf(AdUnitInfoJClass)) return null;

            return new BidonAdUnitInfo
            {
                DemandId = info.SafeCall<string>("getDemandId"),
                Label = info.SafeCall<string>("getLabel"),
                Price = info.SafeCall<AndroidJavaObject>("getPrice")?.SafeCall<double>("doubleValue"),
                Uid = info.SafeCall<string>("getUid"),
                BidType = info.SafeCall<string>("getBidType"),
                FillStartTs = info.SafeCall<AndroidJavaObject>("getFillStartTs")?.SafeCall<long>("longValue"),
                FillFinishTs = info.SafeCall<AndroidJavaObject>("getFillFinishTs")?.SafeCall<long>("longValue"),
                Status = info.SafeCall<string>("getStatus"),
                Ext = info.SafeCall<string>("getExt"),
            };
        }
    }
}
#endif
