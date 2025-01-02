#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonAuctionInfo
    {
        public string AuctionId;
        public string AuctionConfigurationId; // long or null
        public string AuctionConfigurationUid;
        public long   AuctionTimeout;
        public double AuctionPriceFloor;
        public IntPtr NoBidsPtr;
        public IntPtr AdUnitsPtr;
    }
}
#endif
