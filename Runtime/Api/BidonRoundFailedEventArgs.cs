using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonRoundFailedEventArgs : EventArgs
    {
        public string RoundId { get; }

        public BidonError Cause { get; }

        public BidonRoundFailedEventArgs(string roundId, BidonError cause)
        {
            RoundId = roundId;
            Cause = cause;
        }
    }
}
