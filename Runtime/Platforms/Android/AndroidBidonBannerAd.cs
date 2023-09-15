#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class AndroidBidonBannerAd : IBidonBannerAd, IAndroidBannerListener
    {
        private readonly AndroidJavaObject _bannerAdJavaObject;
        private readonly AndroidJavaObject _activityJavaObject;

        internal AndroidBidonBannerAd()
        {
            try
            {
                _bannerAdJavaObject = new AndroidJavaObject("org.bidon.sdk.ads.banner.BannerManager");
                _activityJavaObject = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
                return;
            }

            _bannerAdJavaObject.Call("setBannerListener", new AndroidBannerListener(this));
        }

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        public void SetFormat(BidonBannerFormat format)
        {
            _bannerAdJavaObject?.Call("setBannerFormat", AndroidBidonJavaHelper.GetBannerFormatJavaObject(format));
        }

        public void SetPredefinedPosition(BidonBannerPosition position)
        {
            _bannerAdJavaObject?.Call("setPosition", AndroidBidonJavaHelper.GetBannerPositionJavaObject(position));
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint)
        {
            _bannerAdJavaObject?.Call("setCustomPosition",
                AndroidBidonJavaHelper.GetPointJavaObject(positionOffset),
                rotationAngle,
                AndroidBidonJavaHelper.GetPointFJavaObject(anchorPoint));
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle)
        {
            SetCustomPositionAndRotation(positionOffset, rotationAngle, new Vector2(0.5f, 0.5f));
        }

        public void Load(double priceFloor)
        {
            _bannerAdJavaObject?.Call("loadAd", _activityJavaObject, priceFloor);
        }

        public bool IsReady()
        {
            return _bannerAdJavaObject?.Call<bool>("isReady") ?? false;
        }

        public void Show()
        {
            _bannerAdJavaObject?.Call("showAd", _activityJavaObject);
        }

        public void Hide()
        {
            _bannerAdJavaObject?.Call("hideAd", _activityJavaObject);
        }

        public void Destroy()
        {
            _bannerAdJavaObject?.Call("destroyAd", _activityJavaObject);
        }

        public void SetExtraData(string key, object value)
        {
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _bannerAdJavaObject?.Call("addExtra", key,
                value == null ? null : AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public IDictionary<string, object> GetExtraData()
        {
            return AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_bannerAdJavaObject?.Call<AndroidJavaObject>("getExtras"));
        }

        public void NotifyLoss(string winnerDemandId, double ecpm)
        {
            _bannerAdJavaObject?.Call("notifyLoss", winnerDemandId, ecpm);
        }

        public void NotifyWin()
        {
            _bannerAdJavaObject?.Call("notifyWin");
        }

        #region Callbacks

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

        public void onAdExpired(AndroidJavaObject ad)
        {
            OnAdExpired?.Invoke(this, new BidonAdExpiredEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad)));
        }

        public void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue)
        {
            OnAdRevenueReceived?.Invoke(this, new BidonAdRevenueReceivedEventArgs(AndroidBidonJavaHelper.GetBidonAd(ad), AndroidBidonJavaHelper.GetBidonAdValue(adValue)));
        }

        #endregion
    }
}
#endif
