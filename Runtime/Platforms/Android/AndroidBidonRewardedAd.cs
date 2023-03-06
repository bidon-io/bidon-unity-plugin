#if UNITY_ANDROID
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class AndroidBidonRewardedAd : IBidonRewardedAd, IAndroidRewardedListener
    {
        private readonly AndroidJavaObject _rewardedAdJavaObject;
        private readonly AndroidJavaObject _activityJavaObject;

        internal AndroidBidonRewardedAd(string placement)
        {
            try
            {
                _rewardedAdJavaObject = new AndroidJavaObject("org.bidon.sdk.ads.rewarded.RewardedAd", placement);
                _activityJavaObject = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
                return;
            }

            _rewardedAdJavaObject.Call("setRewardedListener", new AndroidRewardedListener(this));
        }

        public event EventHandler<BidonAuctionStartedEventArgs> OnAuctionStarted;
        public event EventHandler<BidonAuctionSucceedEventArgs> OnAuctionSucceed;
        public event EventHandler<BidonAuctionFailedEventArgs> OnAuctionFailed;

        public event EventHandler<BidonRoundStartedEventArgs> OnRoundStarted;
        public event EventHandler<BidonRoundSucceedEventArgs> OnRoundSucceed;
        public event EventHandler<BidonRoundFailedEventArgs> OnRoundFailed;

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        public event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded;

        public void Load(double priceFloor)
        {
            _rewardedAdJavaObject?.Call("loadAd", _activityJavaObject, priceFloor);
        }

        public bool IsReady()
        {
            return _rewardedAdJavaObject?.Call<bool>("isReady") ?? false;
        }

        public void Show()
        {
            _rewardedAdJavaObject?.Call("showAd", _activityJavaObject);
        }

        public void Destroy()
        {
            _rewardedAdJavaObject?.Call("destroyAd");
        }

        public string GetPlacementId()
        {
            return _rewardedAdJavaObject?.Call<string>("getPlacementId");
        }

        #region Callbacks

        public void onAuctionStarted()
        {
            OnAuctionStarted?.Invoke(this, new BidonAuctionStartedEventArgs());
        }

        public void onAuctionSuccess(AndroidJavaObject auctionResults)
        {
            OnAuctionSucceed?.Invoke(this, new BidonAuctionSucceedEventArgs(AndroidBidonJavaHelper.GetListOfBidonAuctionResults(auctionResults)));
        }

        public void onAuctionFailed(AndroidJavaObject cause)
        {
            OnAuctionFailed?.Invoke(this, new BidonAuctionFailedEventArgs(AndroidBidonJavaHelper.GetBidonError(cause)));
        }

        public void onRoundStarted(string roundId, double priceFloor)
        {
            OnRoundStarted?.Invoke(this, new BidonRoundStartedEventArgs(roundId, priceFloor));
        }

        public void onRoundSucceed(string roundId, AndroidJavaObject roundResults)
        {
            OnRoundSucceed?.Invoke(this, new BidonRoundSucceedEventArgs(roundId, AndroidBidonJavaHelper.GetListOfBidonAuctionResults(roundResults)));
        }

        public void onRoundFailed(string roundId, AndroidJavaObject cause)
        {
            OnRoundFailed?.Invoke(this, new BidonRoundFailedEventArgs(roundId, AndroidBidonJavaHelper.GetBidonError(cause)));
        }

        public void onAdLoaded(AndroidJavaObject ad)
        {
            OnAdLoaded?.Invoke(this, new BidonAdLoadedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onAdLoadFailed(AndroidJavaObject cause)
        {
            OnAdLoadFailed?.Invoke(this, new BidonAdLoadFailedEventArgs(AndroidBidonJavaHelper.GetBidonError(cause)));
        }

        public void onAdShown(AndroidJavaObject ad)
        {
            OnAdShown?.Invoke(this, new BidonAdShownEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onAdShowFailed(AndroidJavaObject cause)
        {
            OnAdShowFailed?.Invoke(this, new BidonAdShowFailedEventArgs(AndroidBidonJavaHelper.GetBidonError(cause)));
        }

        public void onAdClicked(AndroidJavaObject ad)
        {
            OnAdClicked?.Invoke(this, new BidonAdClickedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onAdClosed(AndroidJavaObject ad)
        {
            OnAdClosed?.Invoke(this, new BidonAdClosedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onAdExpired(AndroidJavaObject ad)
        {
            OnAdExpired?.Invoke(this, new BidonAdExpiredEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue)
        {
            OnAdRevenueReceived?.Invoke(this, new BidonAdRevenueReceivedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad), AndroidBidonJavaHelper.GetBidonAdValue(adValue)));
        }

        public void onUserRewarded(AndroidJavaObject ad, AndroidJavaObject reward)
        {
            OnUserRewarded?.Invoke(this, new BidonUserRewardedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad), AndroidBidonJavaHelper.GetBidonReward(reward)));
        }

        #endregion
    }
}
#endif
