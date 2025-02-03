#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonGdprApplicabilityStatusConverter
    {
        private static readonly AndroidJavaClass GdprJClass;

        static AndroidBidonGdprApplicabilityStatusConverter()
        {
            GdprJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.regulation.Gdpr");
        }

        public static BidonGdprApplicabilityStatus ToBidonGdprApplicabilityStatus(this AndroidJavaObject status)
        {
            if (!status.IsValidInstanceOf(GdprJClass)) return BidonGdprApplicabilityStatus.Unknown;

            string statusStr = status.SafeCall<string>("name")?.ToLower();

            return statusStr switch
            {
                "unknown" => BidonGdprApplicabilityStatus.Unknown,
                "doesnotapply" => BidonGdprApplicabilityStatus.DoesNotApply,
                "applies" => BidonGdprApplicabilityStatus.Applies,
#if BIDON_DEV
                _ => throw new ArgumentOutOfRangeException(nameof(statusStr), statusStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonGdprApplicabilityStatus)}'")
#else
                _ => BidonGdprApplicabilityStatus.Unknown
#endif
            };
        }

        public static AndroidJavaObject ToJavaObject(this BidonGdprApplicabilityStatus status)
        {
            if (!Enum.IsDefined(typeof(BidonGdprApplicabilityStatus), status)) LogErrorAndReturnDefault();

            return status switch
            {
                BidonGdprApplicabilityStatus.Unknown => GdprJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Unknown"),
                BidonGdprApplicabilityStatus.DoesNotApply => GdprJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "DoesNotApply"),
                BidonGdprApplicabilityStatus.Applies => GdprJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Applies"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: '{nameof(BidonGdprApplicabilityStatus.Unknown)}'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(status), status, $"Unsupported value received. It cannot be mapped to '{nameof(BidonGdprApplicabilityStatus)}'");
#else
                return BidonGdprApplicabilityStatus.Unknown.ToJavaObject();
#endif
            }
        }
    }
}
#endif
