// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdRevenueReceivedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdValue AdValue { get; }

        public BidonAdRevenueReceivedEventArgs(BidonAd ad, BidonAdValue adValue)
        {
            Ad = ad;
            AdValue = adValue;
        }
    }
}
