#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;
using UnityEngine.Scripting;

namespace Bidon.Mediation
{
    public class AndroidRewardedAdListener : AndroidJavaProxy, IAndroidRewardedAdListener
    {
        private readonly IAndroidRewardedAdListener _listener;

        internal AndroidRewardedAdListener(IAndroidRewardedAdListener listener) : base("org.bidon.sdk.ads.rewarded.RewardedListener")
        {
            _listener = listener;
        }

        [Preserve]
        public void onAdLoaded(AndroidJavaObject ad, AndroidJavaObject auctionInfo)
        {
            SyncContextHelper.Post(obj => _listener?.onAdLoaded(ad, auctionInfo));
        }

        [Preserve]
        public void onAdLoadFailed(AndroidJavaObject auctionInfo, AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onAdLoadFailed(auctionInfo, cause));
        }

        [Preserve]
        public void onAdShown(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdShown(ad));
        }

        [Preserve]
        public void onAdShowFailed(AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onAdShowFailed(cause));
        }

        [Preserve]
        public void onAdClicked(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdClicked(ad));
        }

        [Preserve]
        public void onAdClosed(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdClosed(ad));
        }

        [Preserve]
        public void onAdExpired(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdExpired(ad));
        }

        [Preserve]
        public void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue)
        {
            SyncContextHelper.Post(obj => _listener?.onRevenuePaid(ad, adValue));
        }

        [Preserve]
        public void onUserRewarded(AndroidJavaObject ad, AndroidJavaObject reward)
        {
            SyncContextHelper.Post(obj => _listener?.onUserRewarded(ad, reward));
        }
    }
}
#endif
