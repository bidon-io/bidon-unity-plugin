// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdShowFailedEventArgs : EventArgs
    {
        public BidonError Cause { get; }

        public BidonAdShowFailedEventArgs(BidonError cause)
        {
            Cause = cause;
        }
    }
}
