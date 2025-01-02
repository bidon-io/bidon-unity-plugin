#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonInterstitialAd : IosBidonAdBase, IBidonInterstitialAd
    {
        private static IosBidonInterstitialAd _instance;

        private IntPtr _interstitialAdPtr;
        private IntPtr _interstitialDelegatePtr;

        internal IosBidonInterstitialAd(string auctionKey)
        {
            _instance = this;

            _interstitialDelegatePtr = BidonInterstitialAdDelegateCreate(AdLoaded,
                                                                         AdLoadFailed,
                                                                         AdShown,
                                                                         AdShowFailed,
                                                                         AdClicked,
                                                                         AdClosed,
                                                                         AdExpired,
                                                                         AdRevenueReceived);
            _interstitialAdPtr = BidonInterstitialAdCreate(auctionKey, _interstitialDelegatePtr);
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonInterstitialAdLoad(_interstitialAdPtr, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonInterstitialAdIsReady(_interstitialAdPtr);
        }

        public void Show()
        {
            if (IsDisposed()) return;
            BidonInterstitialAdShow(_interstitialAdPtr);
        }

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;

            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            switch (value)
            {
                case bool valueBool:
                    BidonInterstitialAdSetExtraDataBool(_interstitialAdPtr, key, valueBool);
                    break;
                case char valueChar:
                    BidonInterstitialAdSetExtraDataString(_interstitialAdPtr, key, valueChar.ToString());
                    break;
                case int valueInt:
                    BidonInterstitialAdSetExtraDataInt(_interstitialAdPtr, key, valueInt);
                    break;
                case long valueLong:
                    BidonInterstitialAdSetExtraDataLong(_interstitialAdPtr, key, valueLong);
                    break;
                case float valueFloat:
                    BidonInterstitialAdSetExtraDataFloat(_interstitialAdPtr, key, valueFloat);
                    break;
                case double valueDouble:
                    BidonInterstitialAdSetExtraDataDouble(_interstitialAdPtr, key, valueDouble);
                    break;
                case string valueString:
                    BidonInterstitialAdSetExtraDataString(_interstitialAdPtr, key, valueString);
                    break;
                case null:
                    BidonInterstitialAdSetExtraDataNull(_interstitialAdPtr, key);
                    break;
            }
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonInterstitialAdGetExtraData(_interstitialAdPtr));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            BidonInterstitialAdNotifyLoss(_interstitialAdPtr, winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonInterstitialAdNotifyWin(_interstitialAdPtr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _instance = null;
            }

            if (_interstitialAdPtr != IntPtr.Zero)
            {
                BidonInterstitialAdDestroy(_interstitialAdPtr);
                _interstitialAdPtr = IntPtr.Zero;
            }
            if (_interstitialDelegatePtr != IntPtr.Zero)
            {
                BidonInterstitialAdDelegateDestroy(_interstitialDelegatePtr);
                _interstitialDelegatePtr = IntPtr.Zero;
            }
        }
    }
}
#endif
