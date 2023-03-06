using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAuctionSucceedEventArgs : EventArgs
    {
        public IEnumerable<BidonAuctionResult> AuctionResults { get; }

        public BidonAuctionSucceedEventArgs(IEnumerable<BidonAuctionResult> auctionResults)
        {
            AuctionResults = auctionResults;
        }
    }
}
