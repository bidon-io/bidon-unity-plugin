// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdExpiredEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdExpiredEventArgs(BidonAd ad)
        {
            Ad = ad;
        }
    }
}
