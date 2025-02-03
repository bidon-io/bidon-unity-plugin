// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public class BidonAdUnit
    {
        public string DemandId;

        public string Label;

        public double PriceFloor;

        public string Uid;

        public BidonBidType? BidType;

        [NonSerialized]
        public string ExtJson;

        public string ToJsonString(bool isPretty = false) => UnityEngine.JsonUtility.ToJson(this, isPretty);
    }
}
