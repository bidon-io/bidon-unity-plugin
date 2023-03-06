using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class BidonRewardedAd : IBidonRewardedAd
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

        public event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded;

        private readonly IBidonRewardedAd _bidonRewardedAdImpl;

        public BidonRewardedAd(string placement = "default")
        {
#if UNITY_EDITOR
            _bidonRewardedAdImpl = new EditorBidonRewardedAd(placement);
#elif UNITY_ANDROID
            _bidonRewardedAdImpl = new AndroidBidonRewardedAd(placement);
#elif UNITY_IOS
            _bidonRewardedAdImpl = new IosBidonRewardedAd(placement);
#else
            _bidonRewardedAdImpl = new DummyBidonRewardedAd(placement);
#endif
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            _bidonRewardedAdImpl.OnAuctionStarted += (sender, args) => OnAuctionStarted?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAuctionSucceed += (sender, args) => OnAuctionSucceed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnAuctionFailed += (sender, args) => OnAuctionFailed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnRoundStarted += (sender, args) => OnRoundStarted?.Invoke(this, args);
            _bidonRewardedAdImpl.OnRoundSucceed += (sender, args) => OnRoundSucceed?.Invoke(this, args);
            _bidonRewardedAdImpl.OnRoundFailed += (sender, args) => OnRoundFailed?.Invoke(this, args);
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

        public void Destroy() => _bidonRewardedAdImpl.Destroy();

        public string GetPlacementId() => _bidonRewardedAdImpl.GetPlacementId();
    }
}
