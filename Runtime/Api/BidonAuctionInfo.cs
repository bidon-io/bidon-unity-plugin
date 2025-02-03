// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public class BidonAuctionInfo
    {
        public string AuctionId;

        public long? AuctionConfigurationId;

        public string AuctionConfigurationUid;

        public long AuctionTimeout;

        public double AuctionPriceFloor;

        public IEnumerable<BidonAdUnitInfo> NoBids;

        public IEnumerable<BidonAdUnitInfo> AdUnits;

        public string ToJsonString(bool isPretty = false) => UnityEngine.JsonUtility.ToJson(this, isPretty);
    }
}
