// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdClickedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdClickedEventArgs(BidonAd ad)
        {
            Ad = ad;
        }
    }
}
