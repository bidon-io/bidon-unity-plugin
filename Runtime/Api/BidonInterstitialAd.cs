using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class BidonInterstitialAd : IBidonInterstitialAd
    {
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

        private readonly IBidonInterstitialAd _bidonInterstitialAdImpl;

        public BidonInterstitialAd(string placement = "default")
        {
#if UNITY_EDITOR
            _bidonInterstitialAdImpl = new EditorBidonInterstitialAd(placement);
#elif UNITY_ANDROID
            _bidonInterstitialAdImpl = new AndroidBidonInterstitialAd(placement);
#elif UNITY_IOS
            _bidonInterstitialAdImpl = new IosBidonInterstitialAd(placement);
#else
            _bidonInterstitialAdImpl = new DummyBidonInterstitialAd(placement);
#endif
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            _bidonInterstitialAdImpl.OnAuctionStarted += (sender, args) => OnAuctionStarted?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAuctionSucceed += (sender, args) => OnAuctionSucceed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnAuctionFailed += (sender, args) => OnAuctionFailed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnRoundStarted += (sender, args) => OnRoundStarted?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnRoundSucceed += (sender, args) => OnRoundSucceed?.Invoke(this, args);
            _bidonInterstitialAdImpl.OnRoundFailed += (sender, args) => OnRoundFailed?.Invoke(this, args);
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

        public string GetPlacementId() => _bidonInterstitialAdImpl.GetPlacementId();
    }
}
