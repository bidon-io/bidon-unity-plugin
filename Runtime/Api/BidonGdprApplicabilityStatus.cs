// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum BidonGdprApplicabilityStatus
    {
        Unknown = -1,
        DoesNotApply = 0,
        Applies = 1,
    }
}
