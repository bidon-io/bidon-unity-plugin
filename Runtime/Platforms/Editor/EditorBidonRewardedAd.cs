using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class EditorBidonRewardedAd : IBidonRewardedAd
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

        internal EditorBidonRewardedAd(string placement) { }

        public void Load(double priceFloor)
        {
            throw new NotImplementedException();
        }

        public bool IsReady()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public string GetPlacementId()
        {
            throw new NotImplementedException();
        }
    }
}
