#if (!UNITY_ANDROID && !UNITY_IOS) || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    internal class DummyBidonSegment : IBidonSegment
    {
        public string Uid { get; }
        public int? Age { get; set; }
        public BidonUserGender? Gender { get; set; }
        public int? Level { get; set; }
        public double? TotalInAppsAmount { get; set; }
        public bool IsPaying { get; set; }
        public IDictionary<string, object> CustomAttributes { get; }

        public void SetCustomAttribute(string name, object value)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif
