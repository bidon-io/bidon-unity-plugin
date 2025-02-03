// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public class BidonAd
    {
        public BidonAdUnit AdUnit;

        public string AuctionId;

        public string CurrencyCode;

        public BidonAdType? AdType;

        public string Dsp;

        public double Price;

        public string NetworkName;

        public string ToJsonString(bool isPretty = false) => UnityEngine.JsonUtility.ToJson(this, isPretty);
    }
}
