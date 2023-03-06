using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public class BidonAd
    {
        public string AdUnitId;

        public string AuctionId;

        public string CurrencyCode;

        public BidonDemandAd DemandAd;

        public string Dsp;

        public double Ecpm;

        public string NetworkName;

        public string RoundId;

        public string ToJsonString(bool isPretty = false) => UnityEngine.JsonUtility.ToJson(this, isPretty);
    }
}
