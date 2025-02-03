// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IBidonRegulation
    {
        public BidonGdprApplicabilityStatus GdprApplicabilityStatus { get; set; }
        public string GdprConsentString { get; set; }
        public bool IsGdprApplied { get; }
        public bool HasGdprConsent { get; }

        public string UsPrivacyString { get; set; }
        public bool IsCcpaApplied { get; }
        public bool HasCcpaConsent { get; }

        public BidonCoppaApplicabilityStatus CoppaApplicabilityStatus { get; set; }
        public bool IsCoppaApplied { get; }
    }
}
