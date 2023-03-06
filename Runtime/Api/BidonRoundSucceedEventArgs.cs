using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonRoundSucceedEventArgs : EventArgs
    {
        public string RoundId { get; }

        public IEnumerable<BidonAuctionResult> RoundResults { get; }

        public BidonRoundSucceedEventArgs(string roundId, IEnumerable<BidonAuctionResult> roundResults)
        {
            RoundId = roundId;
            RoundResults = roundResults;
        }
    }
}
