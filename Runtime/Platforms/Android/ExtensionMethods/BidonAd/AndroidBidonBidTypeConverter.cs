#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonBidTypeConverter
    {
        private static readonly AndroidJavaClass BidTypeJClass;

        static AndroidBidonBidTypeConverter()
        {
            BidTypeJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.stats.models.BidType");
        }

        public static BidonBidType? ToBidonBidType(this AndroidJavaObject bidType)
        {
            if (!bidType.IsValidInstanceOf(BidTypeJClass)) return null;

            string bidTypeStr = bidType.SafeCall<string>("name")?.ToLower();

            return bidTypeStr switch
            {
                "cpm" => BidonBidType.Cpm,
                "rtb" => BidonBidType.Rtb,
#if BIDON_DEV
                _ => throw new System.ArgumentOutOfRangeException(nameof(bidTypeStr), bidTypeStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonBidType)}'")
#else
                _ => null
#endif
            };
        }
    }
}
#endif
