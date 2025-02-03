// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public class BidonReward
    {
        public string Label;

        public double Amount;

        public string ToJsonString(bool isPretty = false) => UnityEngine.JsonUtility.ToJson(this, isPretty);
    }
}
