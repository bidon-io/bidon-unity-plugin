#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonBannerFormatConverter
    {
        private static readonly AndroidJavaClass BannerFormatJClass;

        static AndroidBidonBannerFormatConverter()
        {
            BannerFormatJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.banner.BannerFormat");
        }

        public static BidonBannerFormat? ToBidonBannerFormat(this AndroidJavaObject format)
        {
            if (!format.IsValidInstanceOf(BannerFormatJClass)) return null;

            string bannerFormatStr = format.SafeCall<string>("name")?.ToLower();

            return bannerFormatStr switch
            {
                "banner" => BidonBannerFormat.Banner,
                "leaderboard" => BidonBannerFormat.Leaderboard,
                "mrec" => BidonBannerFormat.Mrec,
                "adaptive" => BidonBannerFormat.Adaptive,
#if BIDON_DEV
                _ => throw new ArgumentOutOfRangeException(nameof(bannerFormatStr), bannerFormatStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonBannerFormat)}'")
#else
                _ => null
#endif
            };
        }

        public static AndroidJavaObject ToJavaObject(this BidonBannerFormat format)
        {
            if (!Enum.IsDefined(typeof(BidonBannerFormat), format)) LogErrorAndReturnDefault();

            return format switch
            {
                BidonBannerFormat.Banner => BannerFormatJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Banner"),
                BidonBannerFormat.Leaderboard => BannerFormatJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "LeaderBoard"),
                BidonBannerFormat.Mrec => BannerFormatJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "MRec"),
                BidonBannerFormat.Adaptive => BannerFormatJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Adaptive"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: '{nameof(BidonBannerFormat.Banner)}'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(format), format, $"Unsupported value received. It cannot be mapped to '{nameof(BidonBannerFormat)}'");
#else
                return BidonBannerFormat.Banner.ToJavaObject();
#endif
            }
        }
    }
}
#endif
