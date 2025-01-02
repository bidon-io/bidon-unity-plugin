#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonAdUnit
    {
        public string Uid;
        public string DemandId;
        public string Label;
        public double PriceFloor;
        public int BidType;
        public string ExtJson;
    }
}
#endif
