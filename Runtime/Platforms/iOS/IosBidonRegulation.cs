#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal class IosBidonRegulation : IBidonRegulation
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetGdprApplicabilityStatus")]
        private static extern int BidonRegulationGetGdprApplicabilityStatus();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetGdprApplicabilityStatus")]
        private static extern void BidonRegulationSetGdprApplicabilityStatus(BidonGdprApplicabilityStatus gdprApplicabilityStatus);

        public BidonGdprApplicabilityStatus GdprApplicabilityStatus
        {
            get => BidonRegulationGetGdprApplicabilityStatus().ToEnum(BidonGdprApplicabilityStatus.Unknown);
            set => BidonRegulationSetGdprApplicabilityStatus(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetGdprConsentString")]
        private static extern string BidonRegulationGetGdprConsentString();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetGdprConsentString")]
        private static extern void BidonRegulationSetGdprConsentString(string gdprConsent);

        public string GdprConsentString
        {
            get => BidonRegulationGetGdprConsentString();
            set => BidonRegulationSetGdprConsentString(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetIsGdprApplied")]
        private static extern bool BidonRegulationGetIsGdprApplied();

        public bool IsGdprApplied
        {
            get => BidonRegulationGetIsGdprApplied();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetHasGdprConsent")]
        private static extern bool BidonRegulationGetHasGdprConsent();

        public bool HasGdprConsent
        {
            get => BidonRegulationGetHasGdprConsent();
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetIsCcpaApplied")]
        private static extern bool BidonRegulationGetIsCcpaApplied();

        public bool IsCcpaApplied
        {
            get => BidonRegulationGetIsCcpaApplied();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetHasCcpaConsent")]
        private static extern bool BidonRegulationGetHasCcpaConsent();

        public bool HasCcpaConsent
        {
            get => BidonRegulationGetHasCcpaConsent();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetCoppaApplicabilityStatus")]
        private static extern int BidonRegulationGetCoppaApplicabilityStatus();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationSetCoppaApplicabilityStatus")]
        private static extern void BidonRegulationSetCoppaApplicabilityStatus(BidonCoppaApplicabilityStatus coppaApplicabilityStatus);

        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus
        {
            get => BidonRegulationGetCoppaApplicabilityStatus().ToEnum(BidonCoppaApplicabilityStatus.Unknown);
            set => BidonRegulationSetCoppaApplicabilityStatus(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegulationGetIsCoppaApplied")]
        private static extern bool BidonRegulationGetIsCoppaApplied();

        public bool IsCoppaApplied
        {
            get => BidonRegulationGetIsCoppaApplied();
        }
    }
}
#endif
