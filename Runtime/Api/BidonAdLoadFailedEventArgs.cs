// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
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
