#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAdUnit
    {
        public string Uid;
        public string DemandId;
        public string Label;
        public double PriceFloor;
        public int BidType;
        public string ExtJson;

        public BidonAdUnit ToBidonAdUnit()
        {
            return new BidonAdUnit
            {
                DemandId = DemandId,
                Label = Label,
                PriceFloor = PriceFloor,
                Uid = Uid,
                BidType = (BidonBidType)BidType,
                Timeout = 0, // TODO: replace with actual value / remove after sync between native platforms
                ExtJson = ExtJson,
            };
        }
    }
}
#endif
