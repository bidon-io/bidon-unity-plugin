using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class BidonInterstitialAd : IBidonInterstitialAd
    {
        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        private readonly IBidonInterstitialAd _bidonInterstitialAdImpl;

        public BidonInterstitialAd()
        {
#if UNITY_EDITOR
            _bidonInterstitialAdImpl = new EditorBidonInterstitialAd();
#elif UNITY_ANDROID
            _bidonInterstitialAdImpl = new AndroidBidonInterstitialAd();
#elif UNITY_IOS
            _bidonInterstitialAdImpl = new IosBidonInterstitialAd();
#else
            _bidonInterstitialAdImpl = new DummyBidonInterstitialAd();
#endif
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            _bidonInterstitialAdImpl.OnAdLoaded += (sender, args) => OnAdLoaded?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdLoadFailed += (sender, args) => OnAdLoadFailed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdShown += (sender, args) => OnAdShown?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdShowFailed += (sender, args) => OnAdShowFailed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdClicked += (sender, args) => OnAdClicked?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdClosed += (sender, args) => OnAdClosed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdExpired += (sender, args) => OnAdExpired?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAdRevenueReceived += (sender, args) => OnAdRevenueReceived?.Invoke(this, args);
        }

        public void Load(double priceFloor) => _bidonInterstitialAdImpl.Load(priceFloor);

        public bool IsReady() => _bidonInterstitialAdImpl.IsReady();

        public void Show() => _bidonInterstitialAdImpl.Show();

        public void Destroy() => _bidonInterstitialAdImpl.Destroy();

        public void SetExtraData(string key, object value) => _bidonInterstitialAdImpl.SetExtraData(key, value);

        public IDictionary<string, object> GetExtraData() => _bidonInterstitialAdImpl.GetExtraData();

        public void NotifyLoss(string winnerDemandId, double ecpm) => _bidonInterstitialAdImpl.NotifyLoss(winnerDemandId, ecpm);

        public void NotifyWin() => _bidonInterstitialAdImpl.NotifyWin();
    }
}
