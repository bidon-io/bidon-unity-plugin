#if (!UNITY_ANDROID && !UNITY_EDITOR && !UNITY_IOS) || BIDON_DEV_DUMMY

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class DummyBidonRegulation : IBidonRegulation
    {
        public string GdprConsentString { get; set; }
        public string UsPrivacyString { get; set; }
        public BidonGdprApplicabilityStatus GdprApplicabilityStatus { get; set; }
        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus { get; set; }
    }
}
#endif
