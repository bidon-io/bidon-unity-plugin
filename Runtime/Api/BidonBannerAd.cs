using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class BidonBannerAd : IBidonBannerAd
    {
        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        private readonly IBidonBannerAd _bidonBannerAdImpl;

        public BidonBannerAd()
        {
#if UNITY_EDITOR
            _bidonBannerAdImpl = new EditorBidonBannerAd();
#elif UNITY_ANDROID
            _bidonBannerAdImpl = new AndroidBidonBannerAd();
#elif UNITY_IOS
            _bidonBannerAdImpl = new IosBidonBannerAd();
#else
            _bidonBannerAdImpl = new DummyBidonBannerAd();
#endif
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            _bidonBannerAdImpl.OnAdLoaded += (sender, args) => OnAdLoaded?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdLoadFailed += (sender, args) => OnAdLoadFailed?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdShown += (sender, args) => OnAdShown?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdShowFailed += (sender, args) => OnAdShowFailed?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdClicked += (sender, args) => OnAdClicked?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdExpired += (sender, args) => OnAdExpired?.Invoke(this, args);
            _bidonBannerAdImpl.OnAdRevenueReceived += (sender, args) => OnAdRevenueReceived?.Invoke(this, args);
        }

        public void SetFormat(BidonBannerFormat format) => _bidonBannerAdImpl.SetFormat(format);

        public BidonBannerFormat GetFormat() => _bidonBannerAdImpl.GetFormat();

        public void SetPredefinedPosition(BidonBannerPosition position) =>
            _bidonBannerAdImpl.SetPredefinedPosition(position);

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint) =>
            _bidonBannerAdImpl.SetCustomPositionAndRotation(positionOffset, rotationAngle, anchorPoint);

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle = 0) =>
            _bidonBannerAdImpl.SetCustomPositionAndRotation(positionOffset, rotationAngle);

        public void Load(double priceFloor) => _bidonBannerAdImpl.Load(priceFloor);

        public bool IsReady() => _bidonBannerAdImpl.IsReady();

        public void Show() => _bidonBannerAdImpl.Show();

        public bool IsShowing() => _bidonBannerAdImpl.IsShowing();

        public void Hide() => _bidonBannerAdImpl.Hide();

        public void SetExtraData(string key, object value)
        {
            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;
            _bidonBannerAdImpl.SetExtraData(key, value);
        }

        public IDictionary<string, object> GetExtraData() => _bidonBannerAdImpl.GetExtraData();

        public void NotifyLoss(string winnerDemandId, double ecpm) => _bidonBannerAdImpl.NotifyLoss(winnerDemandId, ecpm);

        public void NotifyWin() => _bidonBannerAdImpl.NotifyWin();

        public void Dispose() => _bidonBannerAdImpl.Dispose();
    }
}
