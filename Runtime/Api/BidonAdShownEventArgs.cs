// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdShownEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdShownEventArgs(BidonAd ad)
        {
            Ad = ad;
        }
    }
}
