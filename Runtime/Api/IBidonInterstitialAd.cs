using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public interface IBidonInterstitialAd
    {
        event EventHandler<BidonAuctionStartedEventArgs> OnAuctionStarted;
        event EventHandler<BidonAuctionSucceedEventArgs> OnAuctionSucceed;
        event EventHandler<BidonAuctionFailedEventArgs> OnAuctionFailed;

        event EventHandler<BidonRoundStartedEventArgs> OnRoundStarted;
        event EventHandler<BidonRoundSucceedEventArgs> OnRoundSucceed;
        event EventHandler<BidonRoundFailedEventArgs> OnRoundFailed;

        event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        event EventHandler<BidonAdShownEventArgs> OnAdShown;
        event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

        event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        void Load(double priceFloor);
        bool IsReady();
        void Show();
        void Destroy();
        string GetPlacementId();
    }
}
