// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonUserRewardedEventArgs : EventArgs
    {
        public BidonAd Ad { get; }
        public BidonReward Reward { get; }

        public BidonUserRewardedEventArgs(BidonAd ad, BidonReward reward)
        {
            Ad = ad;
            Reward = reward;
        }
    }
}
