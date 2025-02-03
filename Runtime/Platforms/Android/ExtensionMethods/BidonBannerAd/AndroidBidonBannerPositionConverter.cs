#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonBannerPositionConverter
    {
        private static readonly AndroidJavaClass BannerPositionJClass;

        static AndroidBidonBannerPositionConverter()
        {
            BannerPositionJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.banner.BannerPosition");
        }

        public static AndroidJavaObject ToJavaObject(this BidonBannerPosition position)
        {
            if (!Enum.IsDefined(typeof(BidonBannerPosition), position)) LogErrorAndReturnDefault();

            return position switch
            {
                BidonBannerPosition.HorizontalTop => BannerPositionJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "HorizontalTop"),
                BidonBannerPosition.HorizontalBottom => BannerPositionJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "HorizontalBottom"),
                BidonBannerPosition.VerticalLeft => BannerPositionJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "VerticalLeft"),
                BidonBannerPosition.VerticalRight => BannerPositionJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "VerticalRight"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: '{nameof(BidonBannerPosition.HorizontalTop)}'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(position), position, $"Unsupported value received. It cannot be mapped to '{nameof(BidonBannerPosition)}'");
#else
                return BidonBannerPosition.HorizontalTop.ToJavaObject();
#endif
            }
        }
    }
}
#endif
