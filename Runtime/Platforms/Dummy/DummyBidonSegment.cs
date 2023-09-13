#if (!UNITY_ANDROID && !UNITY_EDITOR && !UNITY_IOS) || BIDON_DEV_DUMMY
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class DummyBidonSegment : IBidonSegment
    {
        public string Id { get; }
        public int Age { get; set; }
        public BidonUserGender Gender { get; set; }
        public int Level { get; set; }
        public double TotalInAppsAmount { get; set; }
        public bool IsPaying { get; set; }
        public IDictionary<string, object> CustomAttributes { get; }
        public void SetCustomAttribute(string name, object value)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif
