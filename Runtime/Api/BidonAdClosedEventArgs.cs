// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdClosedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdClosedEventArgs(BidonAd ad)
        {
            Ad = ad;
        }
    }
}
