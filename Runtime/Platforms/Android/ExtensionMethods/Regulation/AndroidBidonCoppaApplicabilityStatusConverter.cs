#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonCoppaApplicabilityStatusConverter
    {
        private static readonly AndroidJavaClass CoppaJClass;

        static AndroidBidonCoppaApplicabilityStatusConverter()
        {
            CoppaJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.regulation.Coppa");
        }

        public static BidonCoppaApplicabilityStatus ToBidonCoppaApplicabilityStatus(this AndroidJavaObject status)
        {
            if (!status.IsValidInstanceOf(CoppaJClass)) return BidonCoppaApplicabilityStatus.Unknown;

            string statusStr = status.SafeCall<string>("name")?.ToLower();

            return statusStr switch
            {
                "unknown" => BidonCoppaApplicabilityStatus.Unknown,
                "no" => BidonCoppaApplicabilityStatus.No,
                "yes" => BidonCoppaApplicabilityStatus.Yes,
#if BIDON_DEV
                _ => throw new ArgumentOutOfRangeException(nameof(statusStr), statusStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonCoppaApplicabilityStatus)}'")
#else
                _ => BidonCoppaApplicabilityStatus.Unknown
#endif
            };
        }

        public static AndroidJavaObject ToJavaObject(this BidonCoppaApplicabilityStatus status)
        {
            if (!Enum.IsDefined(typeof(BidonCoppaApplicabilityStatus), status)) LogErrorAndReturnDefault();

            return status switch
            {
                BidonCoppaApplicabilityStatus.Unknown => CoppaJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Unknown"),
                BidonCoppaApplicabilityStatus.No => CoppaJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "No"),
                BidonCoppaApplicabilityStatus.Yes => CoppaJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Yes"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: '{nameof(BidonCoppaApplicabilityStatus.Unknown)}'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(status), status, $"Unsupported value received. It cannot be mapped to '{nameof(BidonCoppaApplicabilityStatus)}'");
#else
                return BidonCoppaApplicabilityStatus.Unknown.ToJavaObject();
#endif
            }
        }
    }
}
#endif
