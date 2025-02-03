#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdValueConverter
    {
        private static readonly AndroidJavaClass AdValueJClass;

        static AndroidBidonAdValueConverter()
        {
            AdValueJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.logs.analytic.AdValue");
        }

        public static BidonAdValue ToBidonAdValue(this AndroidJavaObject adValue)
        {
            if (!adValue.IsValidInstanceOf(AdValueJClass)) return null;

            return new BidonAdValue
            {
                AdRevenue = adValue.SafeCall<double>("getAdRevenue"),
                CurrencyCode = adValue.SafeCall<string>("getCurrency"),
                RevenuePrecision = adValue.SafeCall<AndroidJavaObject>("getPrecision").ToBidonRevenuePrecision(),
            };
        }
    }
}
#endif
