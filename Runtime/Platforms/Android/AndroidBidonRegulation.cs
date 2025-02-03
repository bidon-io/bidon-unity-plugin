#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

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

        public BidonGdprApplicabilityStatus GdprApplicabilityStatus
        {
            get => _bidonRegulationJavaObject.SafeCall<AndroidJavaObject>("getGdpr").ToBidonGdprApplicabilityStatus();
            set => _bidonRegulationJavaObject.SafeCall("setGdpr", value.ToJavaObject());
        }

        public string GdprConsentString
        {
            get => _bidonRegulationJavaObject.SafeCall<string>("getGdprConsentString");
            set => _bidonRegulationJavaObject.SafeCall("setGdprConsentString", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public bool IsGdprApplied
        {
            get => _bidonRegulationJavaObject.SafeCall<bool>("getGdprApplies");
        }

        public bool HasGdprConsent
        {
            get => _bidonRegulationJavaObject.SafeCall<bool>("getHasGdprConsent");
        }

        public string UsPrivacyString
        {
            get => _bidonRegulationJavaObject.SafeCall<string>("getUsPrivacyString");
            set => _bidonRegulationJavaObject.SafeCall("setUsPrivacyString", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public bool IsCcpaApplied
        {
            get => _bidonRegulationJavaObject.SafeCall<bool>("getCcpaApplies");
        }

        public bool HasCcpaConsent
        {
            get => _bidonRegulationJavaObject.SafeCall<bool>("getHasCcpaConsent");
        }

        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus
        {
            get => _bidonRegulationJavaObject.SafeCall<AndroidJavaObject>("getCoppa").ToBidonCoppaApplicabilityStatus();
            set => _bidonRegulationJavaObject.SafeCall("setCoppa", value.ToJavaObject());
        }

        public bool IsCoppaApplied
        {
            get => _bidonRegulationJavaObject.SafeCall<bool>("getCoppaApplies");
        }
    }
}
#endif
