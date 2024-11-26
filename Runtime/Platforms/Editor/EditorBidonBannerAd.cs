#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class EditorBidonBannerAd : IBidonBannerAd
    {
        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        internal EditorBidonBannerAd() { }

        public void SetFormat(BidonBannerFormat format)
        {
            throw new NotImplementedException();
        }

        public BidonBannerFormat GetFormat()
        {
            throw new NotImplementedException();
        }

        public BidonBannerSize GetSize()
        {
            throw new NotImplementedException();
        }

        public void SetPredefinedPosition(BidonBannerPosition position)
        {
            throw new NotImplementedException();
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint)
        {
            throw new NotImplementedException();
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle)
        {
            throw new NotImplementedException();
        }

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

        public bool IsShowing()
        {
            throw new NotImplementedException();
        }

        public void Hide()
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
