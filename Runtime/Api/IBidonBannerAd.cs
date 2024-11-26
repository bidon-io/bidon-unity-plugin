using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public interface IBidonBannerAd : IDisposable
    {
        event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        event EventHandler<BidonAdShownEventArgs> OnAdShown;
        event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        void SetFormat(BidonBannerFormat format);
        BidonBannerFormat GetFormat();
        BidonBannerSize GetSize();
        void SetPredefinedPosition(BidonBannerPosition position);
        void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint);
        void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle);
        void Load(double priceFloor);
        bool IsReady();
        void Show();
        bool IsShowing();
        void Hide();
        void SetExtraData(string key, object value);
        IDictionary<string, object> GetExtraData();
        void NotifyLoss(string winnerDemandId, double ecpm);
        void NotifyWin();
    }
}
