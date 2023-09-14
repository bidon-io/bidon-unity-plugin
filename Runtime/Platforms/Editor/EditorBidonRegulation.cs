#if UNITY_EDITOR

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class EditorBidonRegulation : IBidonRegulation
    {
        public string GdprConsentString { get; set; }
        public string UsPrivacyString { get; set; }
        public BidonGdprConsentStatus GdprConsentStatus { get; set; }
        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus { get; set; }
    }
}
#endif
