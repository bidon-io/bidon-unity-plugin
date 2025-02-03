#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonBannerSizeConverter
    {
        private static readonly AndroidJavaClass AdSizeJClass;

        static AndroidBidonBannerSizeConverter()
        {
            AdSizeJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.banner.AdSize");
        }

        public static BidonBannerSize ToBidonBannerSize(this AndroidJavaObject adSize)
        {
            if (!adSize.IsValidInstanceOf(AdSizeJClass)) return null;

            return new BidonBannerSize
            {
                Width = adSize.SafeCall<int>("getWidthDp"),
                Height = adSize.SafeCall<int>("getHeightDp"),
            };
        }
    }
}
#endif
