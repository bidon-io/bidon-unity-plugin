// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdLoadedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAuctionInfo AuctionInfo { get; }

        public BidonAdLoadedEventArgs(BidonAd ad, BidonAuctionInfo auctionInfo)
        {
            Ad = ad;
            AuctionInfo = auctionInfo;
        }
    }
}
