#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdTypeConverter
    {
        private static readonly AndroidJavaClass AdTypeJClass;

        static AndroidBidonAdTypeConverter()
        {
            AdTypeJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.AdType");
        }

        public static BidonAdType? ToBidonAdType(this AndroidJavaObject adType)
        {
            if (!adType.IsValidInstanceOf(AdTypeJClass)) return null;

            string adTypeStr = adType.SafeCall<string>("name")?.ToLower();

            return adTypeStr switch
            {
                "banner" => BidonAdType.Banner,
                "interstitial" => BidonAdType.Interstitial,
                "rewarded" => BidonAdType.Rewarded,
#if BIDON_DEV
                _ => throw new System.ArgumentOutOfRangeException(nameof(adTypeStr), adTypeStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonAdType)}'")
#else
                _ => null
#endif
            };
        }
    }
}
#endif
