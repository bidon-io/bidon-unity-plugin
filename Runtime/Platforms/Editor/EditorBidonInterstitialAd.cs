#if UNITY_EDITOR

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    internal class EditorBidonInterstitialAd : IBidonInterstitialAd
    {
#pragma warning disable CS0067
        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;
#pragma warning restore CS0067

        internal EditorBidonInterstitialAd(string auctionKey) { }

        public void Load(double priceFloor) { }

        public bool IsReady() => false;

        public void Show() { }

        public void SetExtraData(string key, object value) { }

        public IDictionary<string, object> GetExtraData() => new Dictionary<string, object>();

        public void NotifyLoss(string winnerDemandId, double price) { }

        public void NotifyWin() { }

        public void Dispose() { }
    }
}
#endif
