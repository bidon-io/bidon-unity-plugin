#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonPrecisionConverter
    {
        private static readonly AndroidJavaClass PrecisionJClass;

        static AndroidBidonPrecisionConverter()
        {
            PrecisionJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.logs.analytic.Precision");
        }

        public static BidonRevenuePrecision? ToBidonRevenuePrecision(this AndroidJavaObject precision)
        {
            if (!precision.IsValidInstanceOf(PrecisionJClass)) return null;

            string precisionStr = precision.SafeCall<string>("name")?.ToLower();

            return precisionStr switch
            {
                "precise" => BidonRevenuePrecision.Precise,
                "estimated" => BidonRevenuePrecision.Estimated,
#if BIDON_DEV
                _ => throw new System.ArgumentOutOfRangeException(nameof(precisionStr), precisionStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonRevenuePrecision)}'")
#else
                _ => null
#endif
            };
        }
    }
}
#endif
