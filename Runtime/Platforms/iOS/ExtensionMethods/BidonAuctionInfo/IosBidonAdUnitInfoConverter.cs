#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;

namespace Bidon.Mediation
{
    internal static class IosBidonAdUnitInfoConverter
    {
        public static BidonAdUnitInfo ToBidonAdUnitInfo(this IntPtr ptr)
        {
            var iosBidonAdUnitInfo = IosBidonMarshal.SafePtrToStructure<IosBidonAdUnitInfo>(ptr);
            var bidonAdUnitInfo = iosBidonAdUnitInfo?.ToBidonAdUnitInfo();
            return bidonAdUnitInfo;
        }

        private static BidonAdUnitInfo ToBidonAdUnitInfo(this IosBidonAdUnitInfo iosBidonAdUnitInfo)
        {
            return new BidonAdUnitInfo
            {
                DemandId = iosBidonAdUnitInfo.DemandId,
                Label = iosBidonAdUnitInfo.Label,
                Price = iosBidonAdUnitInfo.Price.ToNullableDouble(),
                Uid = iosBidonAdUnitInfo.Uid,
                BidType = iosBidonAdUnitInfo.BidType,
                FillStartTs = iosBidonAdUnitInfo.FillStartTs.ToNullableLong(),
                FillFinishTs = iosBidonAdUnitInfo.FillFinishTs.ToNullableLong(),
                Status = iosBidonAdUnitInfo.Status,
                Ext = iosBidonAdUnitInfo.Ext,
            };
        }
    }
}
#endif
