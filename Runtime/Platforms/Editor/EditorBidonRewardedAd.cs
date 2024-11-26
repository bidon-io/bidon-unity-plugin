#if UNITY_EDITOR
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class EditorBidonRewardedAd : IBidonRewardedAd
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

        internal EditorBidonRewardedAd(string auctionKey) { }

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

        public void SetExtraData(string key, object value)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetExtraData()
        {
            throw new NotImplementedException();
        }

        public void NotifyLoss(string winnerDemandId, double ecpm)
        {
            throw new NotImplementedException();
        }

        public void NotifyWin()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
#endif
