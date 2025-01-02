#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonAdRevenue
    {
        public double Revenue;
        public int RevenuePrecision;
        public string Currency;
    }
}
#endif
