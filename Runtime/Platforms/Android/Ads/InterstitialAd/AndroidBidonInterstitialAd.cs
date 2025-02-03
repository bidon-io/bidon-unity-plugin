#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal partial class AndroidBidonInterstitialAd : AndroidBidonAdBase, IBidonInterstitialAd
    {
        private AndroidJavaObject _interstitialAdJavaObject;

        internal AndroidBidonInterstitialAd(string auctionKey)
        {
            _interstitialAdJavaObject = AndroidBidonFactory.SafeCreateJavaObject("org.bidon.sdk.ads.interstitial.InterstitialAd", auctionKey);
            if (_interstitialAdJavaObject == null) return;
            SetListener();
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            _interstitialAdJavaObject.SafeCall("loadAd", ActivityJavaObject, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return _interstitialAdJavaObject.SafeCall<bool>("isReady");
        }

        public void Show()
        {
            if (IsDisposed()) return;
            _interstitialAdJavaObject.SafeCall("showAd", ActivityJavaObject);
        }

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;

            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _interstitialAdJavaObject.SafeCall("addExtra", key, AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_interstitialAdJavaObject.SafeCall<AndroidJavaObject>("getExtras"));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            _interstitialAdJavaObject.SafeCall("notifyLoss", winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            _interstitialAdJavaObject.SafeCall("notifyWin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _interstitialAdJavaObject.SafeCall("destroyAd");
                _interstitialAdJavaObject?.Dispose();
                _interstitialAdJavaObject = null;
            }

            base.Dispose(disposing);
        }
    }
}
#endif
