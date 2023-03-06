#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidRewardedListener : AndroidJavaProxy, IAndroidRewardedListener
    {
        private readonly IAndroidRewardedListener _listener;

        internal AndroidRewardedListener(IAndroidRewardedListener listener) : base("org.bidon.sdk.ads.rewarded.RewardedListener")
        {
            _listener = listener;
        }

        public void onAuctionStarted()
        {
            SyncContextHelper.Post(obj => _listener?.onAuctionStarted());
        }

        public void onAuctionSuccess(AndroidJavaObject auctionResults)
        {
            SyncContextHelper.Post(obj => _listener?.onAuctionSuccess(auctionResults));
        }

        public void onAuctionFailed(AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onAuctionFailed(cause));
        }

        public void onRoundStarted(string roundId, double priceFloor)
        {
            SyncContextHelper.Post(obj => _listener?.onRoundStarted(roundId, priceFloor));
        }

        public void onRoundSucceed(string roundId, AndroidJavaObject roundResults)
        {
            SyncContextHelper.Post(obj => _listener?.onRoundSucceed(roundId, roundResults));
        }

        public void onRoundFailed(string roundId, AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onRoundFailed(roundId, cause));
        }

        public void onAdLoaded(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdLoaded(ad));
        }

        public void onAdLoadFailed(AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onAdLoadFailed(cause));
        }

        public void onAdShown(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdShown(ad));
        }

        public void onAdShowFailed(AndroidJavaObject cause)
        {
            SyncContextHelper.Post(obj => _listener?.onAdShowFailed(cause));
        }

        public void onAdClicked(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdClicked(ad));
        }

        public void onAdClosed(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdClosed(ad));
        }

        public void onAdExpired(AndroidJavaObject ad)
        {
            SyncContextHelper.Post(obj => _listener?.onAdExpired(ad));
        }

        public void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue)
        {
            SyncContextHelper.Post(obj => _listener?.onRevenuePaid(ad, adValue));
        }

        public void onUserRewarded(AndroidJavaObject ad, AndroidJavaObject reward)
        {
            SyncContextHelper.Post(obj => _listener?.onUserRewarded(ad, reward));
        }
    }
}
#endif
