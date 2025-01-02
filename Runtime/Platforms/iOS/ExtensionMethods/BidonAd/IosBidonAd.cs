#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonAd
    {
        public IntPtr AdUnitPtr;
        public string AuctionId;
        public string CurrencyCode;
        public int AdType;
        public string Dsp;
        public double Price;
        public string NetworkName;
    }
}
#endif
