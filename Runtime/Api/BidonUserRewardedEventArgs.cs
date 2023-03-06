using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
