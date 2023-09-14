#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class IosBidonRegulation : IBidonRegulation
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetGdprConsentString")]
        private static extern string BidonRegulationGetGdprConsentString();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetGdprConsentString")]
        private static extern void BidonRegulationSetGdprConsentString(string gdprConsent);

        public string GdprConsentString
        {
            get => BidonRegulationGetGdprConsentString();
            set => BidonRegulationSetGdprConsentString(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetUsPrivacyString")]
        private static extern string BidonRegulationGetUsPrivacyString();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetUsPrivacyString")]
        private static extern void BidonRegulationSetUsPrivacyString(string usPrivacy);

        public string UsPrivacyString
        {
            get => BidonRegulationGetUsPrivacyString();
            set => BidonRegulationSetUsPrivacyString(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetGdprConsentStatus")]
        private static extern BidonGdprConsentStatus BidonRegulationGetGdprConsentStatus();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetGdprConsentStatus")]
        private static extern void BidonRegulationSetGdprConsentStatus(BidonGdprConsentStatus gdprConsentStatus);

        public BidonGdprConsentStatus GdprConsentStatus
        {
            get => BidonRegulationGetGdprConsentStatus();
            set => BidonRegulationSetGdprConsentStatus(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetCoppaApplicabilityStatus")]
        private static extern BidonCoppaApplicabilityStatus BidonRegulationGetCoppaApplicabilityStatus();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetCoppaApplicabilityStatus")]
        private static extern void BidonRegulationSetCoppaApplicabilityStatus(BidonCoppaApplicabilityStatus coppaApplicabilityStatus);

        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus
        {
            get => BidonRegulationGetCoppaApplicabilityStatus();
            set => BidonRegulationSetCoppaApplicabilityStatus(value);
        }
    }
}
#endif
