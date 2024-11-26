using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdLoadFailedEventArgs : EventArgs
    {
        public BidonAuctionInfo AuctionInfo { get; }

        public BidonError Cause { get; }

        public BidonAdLoadFailedEventArgs(BidonAuctionInfo auctionInfo, BidonError cause)
        {
            AuctionInfo = auctionInfo;
            Cause = cause;
        }
    }
}
