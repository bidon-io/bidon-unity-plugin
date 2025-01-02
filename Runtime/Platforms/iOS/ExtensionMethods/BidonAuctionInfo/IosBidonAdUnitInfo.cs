#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonAdUnitInfo
    {
        public string DemandId;
        public string Label;
        public string Price; // double or null
        public string Uid;
        public string BidType;
        public string FillStartTs; // long or null
        public string FillFinishTs; // long or null
        public string Status;
        public string Ext;
    }
}
#endif
