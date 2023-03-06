using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonAdLoadedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }

        public BidonAdLoadedEventArgs(BidonAd ad)
        {
            Ad = ad;
        }
    }
}
