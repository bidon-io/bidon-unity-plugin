#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;

namespace Bidon.Mediation
{
    internal static class IosBidonAdUnitConverter
    {
        public static BidonAdUnit ToBidonAdUnit(this IntPtr ptr)
        {
            var iosBidonAdUnit = IosBidonMarshal.SafePtrToStructure<IosBidonAdUnit>(ptr);
            var bidonAdUnit = iosBidonAdUnit?.ToBidonAdUnit();
            return bidonAdUnit;
        }

        private static BidonAdUnit ToBidonAdUnit(this IosBidonAdUnit iosBidonAdUnit)
        {
            return new BidonAdUnit
            {
                DemandId = iosBidonAdUnit.DemandId,
                Label = iosBidonAdUnit.Label,
                PriceFloor = iosBidonAdUnit.PriceFloor,
                Uid = iosBidonAdUnit.Uid,
                BidType = iosBidonAdUnit.BidType.ToNullableEnum<BidonBidType>(),
                ExtJson = iosBidonAdUnit.ExtJson,
            };
        }
    }
}
#endif
