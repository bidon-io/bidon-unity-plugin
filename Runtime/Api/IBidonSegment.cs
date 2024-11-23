using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IBidonSegment
    {
        string Uid { get; }
        int Age { get; set; }
        BidonUserGender Gender { get; set; }
        int Level { get; set; }
        double TotalInAppsAmount { get; set; }
        bool IsPaying { get; set; }
        IDictionary<string, object> CustomAttributes { get; }
        void SetCustomAttribute(string name, object value);
    }
}
