using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IBidonRegulation
    {
        public string GdprConsentString { get; set; }
        public string UsPrivacyString { get; set; }
        public BidonGdprConsentStatus GdprConsentStatus { get; set; }
        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus { get; set; }
    }
}
