using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAuctionFailedEventArgs : EventArgs
    {
        public BidonError Cause { get; }

        public BidonAuctionFailedEventArgs(BidonError cause)
        {
            Cause = cause;
        }
    }
}
