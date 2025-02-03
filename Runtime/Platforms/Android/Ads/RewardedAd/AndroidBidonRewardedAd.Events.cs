#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Bidon.Mediation
{
    internal partial class AndroidBidonRewardedAd : IAndroidRewardedAdListener
    {
        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;
        public event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded;

        [Preserve]
        public void onAdLoaded(AndroidJavaObject ad, AndroidJavaObject auctionInfo)
        {
            OnAdLoaded?.Invoke(this, new BidonAdLoadedEventArgs(ad.ToBidonAd(), auctionInfo.ToBidonAuctionInfo()));
        }

        [Preserve]
        public void onAdLoadFailed(AndroidJavaObject auctionInfo, AndroidJavaObject cause)
        {
            OnAdLoadFailed?.Invoke(this, new BidonAdLoadFailedEventArgs(auctionInfo.ToBidonAuctionInfo(), cause.ToBidonError()));
        }

        [Preserve]
        public void onAdShown(AndroidJavaObject ad)
        {
            OnAdShown?.Invoke(this, new BidonAdShownEventArgs(ad.ToBidonAd()));
        }

        [Preserve]
        public void onAdShowFailed(AndroidJavaObject cause)
        {
            OnAdShowFailed?.Invoke(this, new BidonAdShowFailedEventArgs(cause.ToBidonError()));
        }

        [Preserve]
        public void onAdClicked(AndroidJavaObject ad)
        {
            OnAdClicked?.Invoke(this, new BidonAdClickedEventArgs(ad.ToBidonAd()));
        }

        [Preserve]
        public void onAdClosed(AndroidJavaObject ad)
        {
            OnAdClosed?.Invoke(this, new BidonAdClosedEventArgs(ad.ToBidonAd()));
        }

        [Preserve]
        public void onAdExpired(AndroidJavaObject ad)
        {
            OnAdExpired?.Invoke(this, new BidonAdExpiredEventArgs(ad.ToBidonAd()));
        }

        [Preserve]
        public void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue)
        {
            OnAdRevenueReceived?.Invoke(this, new BidonAdRevenueReceivedEventArgs(ad.ToBidonAd(), adValue.ToBidonAdValue()));
        }

        [Preserve]
        public void onUserRewarded(AndroidJavaObject ad, AndroidJavaObject reward)
        {
            OnUserRewarded?.Invoke(this, new BidonUserRewardedEventArgs(ad.ToBidonAd(), reward.ToBidonReward()));
        }

        private void SetListener()
        {
            _rewardedAdJavaObject.SafeCall("setRewardedListener", new AndroidRewardedAdListener(this));
        }
    }
}
#endif
