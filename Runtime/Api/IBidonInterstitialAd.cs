// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface IBidonInterstitialAd : IDisposable
    {
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
        void SetExtraData(string key, object value);
        IDictionary<string, object> GetExtraData();
        void NotifyLoss(string winnerDemandId, double price);
        void NotifyWin();
    }
}
