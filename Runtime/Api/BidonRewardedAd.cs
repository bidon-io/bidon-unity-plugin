using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class BidonRewardedAd : IBidonRewardedAd
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

        private readonly IBidonRewardedAd _bidonRewardedAdImpl;

        public BidonRewardedAd(string auctionKey = null)
        {
#if UNITY_EDITOR
            _bidonRewardedAdImpl = new EditorBidonRewardedAd(auctionKey);
#elif UNITY_ANDROID
            _bidonRewardedAdImpl = new AndroidBidonRewardedAd(auctionKey);
#elif UNITY_IOS
            _bidonRewardedAdImpl = new IosBidonRewardedAd(auctionKey);
#else
            _bidonRewardedAdImpl = new DummyBidonRewardedAd(auctionKey);
#endif
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            _bidonRewardedAdImpl.OnAdLoaded += (sender, args) => OnAdLoaded?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdLoadFailed += (sender, args) => OnAdLoadFailed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdShown += (sender, args) => OnAdShown?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdShowFailed += (sender, args) => OnAdShowFailed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdClicked += (sender, args) => OnAdClicked?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdClosed += (sender, args) => OnAdClosed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdExpired += (sender, args) => OnAdExpired?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAdRevenueReceived += (sender, args) => OnAdRevenueReceived?.Invoke(this, args);
            _bidonRewardedAdImpl.OnUserRewarded += (sender, args) => OnUserRewarded?.Invoke(this, args);
        }

        public void Load(double priceFloor) => _bidonRewardedAdImpl.Load(priceFloor);

        public bool IsReady() => _bidonRewardedAdImpl.IsReady();

        public void Show() => _bidonRewardedAdImpl.Show();

        public void SetExtraData(string key, object value)
        {
            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;
            _bidonRewardedAdImpl.SetExtraData(key, value);
        }

        public IDictionary<string, object> GetExtraData() => _bidonRewardedAdImpl.GetExtraData();

        public void NotifyLoss(string winnerDemandId, double ecpm) => _bidonRewardedAdImpl.NotifyLoss(winnerDemandId, ecpm);

        public void NotifyWin() => _bidonRewardedAdImpl.NotifyWin();

        public void Dispose() => _bidonRewardedAdImpl.Dispose();
    }
}
