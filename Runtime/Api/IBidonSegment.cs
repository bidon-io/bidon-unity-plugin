using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public interface IBidonSegment
    {
        string Id { get; }
        int Age { get; set; }
        BidonUserGender Gender { get; set; }
        int Level { get; set; }
        double TotalInAppsAmount { get; set; }
        bool IsPaying { get; set; }
        IDictionary<string, object> CustomAttributes { get; }
        void SetCustomAttribute(string name, object value);
    }
}
