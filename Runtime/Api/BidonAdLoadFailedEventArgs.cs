using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdLoadFailedEventArgs : EventArgs
    {
        public BidonError Cause { get; }

        public BidonAdLoadFailedEventArgs(BidonError cause)
        {
            Cause = cause;
        }
    }
}
