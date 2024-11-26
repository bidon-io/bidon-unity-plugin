#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAdUnitInfo
    {
        public string DemandId;
        public string Label;
        public double Price;
        public string Uid;
        public string BidType;
        public long FillStartTs;
        public long FillFinishTs;
        public string Status;
        public string Ext;

        public BidonAdUnitInfo ToBidonAdUnitInfo()
        {
            return new BidonAdUnitInfo
            {
                DemandId = DemandId,
                Label = Label,
                Price = Price,
                Uid = Uid,
                BidType = BidType,
                FillStartTs = FillStartTs,
                FillFinishTs = FillFinishTs,
                Timeout = 0,
                Status = Status,
                ErrorMessage = "",
                Ext = Ext,
            };
        }
    }
}
#endif
