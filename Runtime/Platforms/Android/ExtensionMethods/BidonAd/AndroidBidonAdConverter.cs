#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdConverter
    {
        private static readonly AndroidJavaClass AdJClass;

        static AndroidBidonAdConverter()
        {
            AdJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.Ad");
        }

        public static BidonAd ToBidonAd(this AndroidJavaObject ad)
        {
            if (!ad.IsValidInstanceOf(AdJClass)) return null;

            return new BidonAd
            {
                AdUnit = ad.SafeCall<AndroidJavaObject>("getAdUnit").ToBidonAdUnit(),
                AuctionId = ad.SafeCall<string>("getAuctionId"),
                CurrencyCode = ad.SafeCall<string>("getCurrencyCode"),
                AdType = ad.SafeCall<AndroidJavaObject>("getDemandAd").SafeCall<AndroidJavaObject>("getAdType").ToBidonAdType(),
                Dsp = ad.SafeCall<string>("getDsp"),
                Price = ad.SafeCall<double>("getPrice"),
                NetworkName = ad.SafeCall<string>("getNetworkName"),
            };
        }
    }
}
#endif
