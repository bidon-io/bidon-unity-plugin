// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

        public BidonBannerAd(string auctionKey = BidonConstants.DefaultAuctionKey)
        {
#if UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            _bidonBannerAdImpl = new EditorBidonBannerAd(auctionKey);
#elif UNITY_ANDROID
            _bidonBannerAdImpl = new AndroidBidonBannerAd(auctionKey);
#elif UNITY_IOS
            _bidonBannerAdImpl = new IosBidonBannerAd(auctionKey);
#else
            _bidonBannerAdImpl = new DummyBidonBannerAd(auctionKey);
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

        public BidonBannerFormat? GetFormat() => _bidonBannerAdImpl.GetFormat();

        public BidonBannerSize GetSize() => _bidonBannerAdImpl.GetSize();

        public void SetPredefinedPosition(BidonBannerPosition position) =>
            _bidonBannerAdImpl.SetPredefinedPosition(position);

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint) =>
            _bidonBannerAdImpl.SetCustomPositionAndRotation(positionOffset, rotationAngle, anchorPoint);

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle = 0) =>
            _bidonBannerAdImpl.SetCustomPositionAndRotation(positionOffset, rotationAngle);

        public void Load(double priceFloor = BidonConstants.DefaultPriceFloor) => _bidonBannerAdImpl.Load(priceFloor);

        public bool IsReady() => _bidonBannerAdImpl.IsReady();

        public void Show() => _bidonBannerAdImpl.Show();

        public bool IsShowing() => _bidonBannerAdImpl.IsShowing();

        public void Hide() => _bidonBannerAdImpl.Hide();

        public void SetExtraData(string key, object value) => _bidonBannerAdImpl.SetExtraData(key, value);

        public IDictionary<string, object> GetExtraData() => _bidonBannerAdImpl.GetExtraData();

        public void NotifyLoss(string winnerDemandId, double price) => _bidonBannerAdImpl.NotifyLoss(winnerDemandId, price);

        public void NotifyWin() => _bidonBannerAdImpl.NotifyWin();

        public void Dispose() => _bidonBannerAdImpl.Dispose();
    }
}
