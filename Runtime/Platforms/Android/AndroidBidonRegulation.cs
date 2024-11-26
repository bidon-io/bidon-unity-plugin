#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class AndroidBidonRegulation : IBidonRegulation
    {
        private readonly AndroidJavaObject _bidonRegulationJavaObject;

        internal AndroidBidonRegulation(AndroidJavaObject bidonRegulationJavaObject)
        {
            _bidonRegulationJavaObject = bidonRegulationJavaObject;
        }

        public string GdprConsentString
        {
            get => _bidonRegulationJavaObject?.Call<string>("getGdprConsentString") ?? "";
            set => _bidonRegulationJavaObject?.Call("setGdprConsentString",
                AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public string UsPrivacyString
        {
            get => _bidonRegulationJavaObject?.Call<string>("getUsPrivacyString") ?? "";
            set => _bidonRegulationJavaObject?.Call("setUsPrivacyString",
                AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public BidonGdprApplicabilityStatus GdprApplicabilityStatus
        {
            get
            {
                string nativeGdprApplicabilityStatus = _bidonRegulationJavaObject?.
                    Call<AndroidJavaObject>("getGdpr").
                    Call<string>("name");

                return nativeGdprApplicabilityStatus switch
                {
                    "Unknown" => BidonGdprApplicabilityStatus.Unknown,
                    "DoesNotApply" => BidonGdprApplicabilityStatus.DoesNotApply,
                    "Applies" => BidonGdprApplicabilityStatus.Applies,
                    _ => throw new ArgumentOutOfRangeException(nameof(nativeGdprApplicabilityStatus), nativeGdprApplicabilityStatus, null)
                };
            }
            set => _bidonRegulationJavaObject?.Call("setGdpr", AndroidBidonJavaHelper.GetGdprApplicabilityStatusJavaObject(value));
        }

        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus
        {
            get
            {
                string nativeCoppaApplicabilityStatus = _bidonRegulationJavaObject?.
                    Call<AndroidJavaObject>("getCoppa").
                    Call<string>("name");

                return nativeCoppaApplicabilityStatus switch
                {
                    "Unknown" => BidonCoppaApplicabilityStatus.Unknown,
                    "No" => BidonCoppaApplicabilityStatus.No,
                    "Yes" => BidonCoppaApplicabilityStatus.Yes,
                    _ => throw new ArgumentOutOfRangeException(nameof(nativeCoppaApplicabilityStatus), nativeCoppaApplicabilityStatus, null)
                };
            }
            set => _bidonRegulationJavaObject?.Call("setCoppa", AndroidBidonJavaHelper.GetCoppaApplicabilityStatusJavaObject(value));
        }
    }
}
#endif
