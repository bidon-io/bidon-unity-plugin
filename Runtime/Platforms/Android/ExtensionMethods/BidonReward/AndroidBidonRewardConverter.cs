#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonRewardConverter
    {
        private static readonly AndroidJavaClass RewardJClass;

        static AndroidBidonRewardConverter()
        {
            RewardJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.ads.rewarded.Reward");
        }

        public static BidonReward ToBidonReward(this AndroidJavaObject reward)
        {
            if (!reward.IsValidInstanceOf(RewardJClass)) return null;

            return new BidonReward
            {
                Amount = reward.SafeCall<int>("getAmount"),
                Label = reward.SafeCall<string>("getLabel"),
            };
        }
    }
}
#endif
