#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonBannerAd : IosBidonAdBase, IBidonBannerAd
    {
        private static IosBidonBannerAd _instance;

        private IntPtr _bannerAdPtr;
        private IntPtr _bannerDelegatePtr;

        internal IosBidonBannerAd(string auctionKey)
        {
            _instance = this;

            _bannerDelegatePtr = BidonBannerAdDelegateCreate(AdLoaded,
                                                             AdLoadFailed,
                                                             AdShown,
                                                             AdShowFailed,
                                                             AdClicked,
                                                             AdExpired,
                                                             AdRevenueReceived);
            _bannerAdPtr = BidonBannerAdCreate(auctionKey, _bannerDelegatePtr);
        }

        public void SetFormat(BidonBannerFormat format)
        {
            if (IsDisposed()) return;
            BidonBannerAdSetFormat(_bannerAdPtr, format);
        }

        public BidonBannerFormat? GetFormat()
        {
            if (IsDisposed()) return null;
            return BidonBannerAdGetFormat(_bannerAdPtr).ToNullableEnum<BidonBannerFormat>();
        }

        public BidonBannerSize GetSize()
        {
            if (IsDisposed()) return null;
            var iosBidonBannerSizePtr = BidonBannerAdGetSize(_bannerAdPtr);
            return iosBidonBannerSizePtr.ToBidonBannerSize();
        }

        public void SetPredefinedPosition(BidonBannerPosition position)
        {
            if (IsDisposed()) return;
            BidonBannerAdSetPredefinedPosition(_bannerAdPtr, position);
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint)
        {
            if (IsDisposed()) return;
            BidonBannerAdSetCustomPositionAndRotation(_bannerAdPtr, positionOffset.x, positionOffset.y,
                                                rotationAngle, anchorPoint.x, anchorPoint.y);
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle)
        {
            if (IsDisposed()) return;
            SetCustomPositionAndRotation(positionOffset, rotationAngle, new Vector2(0.5f, 0.5f));
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonBannerAdLoad(_bannerAdPtr, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonBannerAdIsReady(_bannerAdPtr);
        }

        public void Show()
        {
            if (IsDisposed()) return;
            BidonBannerAdShow(_bannerAdPtr);
        }

        public bool IsShowing()
        {
            if (IsDisposed()) return false;
            return BidonBannerAdIsShowing(_bannerAdPtr);
        }

        public void Hide()
        {
            if (IsDisposed()) return;
            BidonBannerAdHide(_bannerAdPtr);
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
                    BidonBannerAdSetExtraDataBool(_bannerAdPtr, key, valueBool);
                    break;
                case char valueChar:
                    BidonBannerAdSetExtraDataString(_bannerAdPtr, key, valueChar.ToString());
                    break;
                case int valueInt:
                    BidonBannerAdSetExtraDataInt(_bannerAdPtr, key, valueInt);
                    break;
                case long valueLong:
                    BidonBannerAdSetExtraDataLong(_bannerAdPtr, key, valueLong);
                    break;
                case float valueFloat:
                    BidonBannerAdSetExtraDataFloat(_bannerAdPtr, key, valueFloat);
                    break;
                case double valueDouble:
                    BidonBannerAdSetExtraDataDouble(_bannerAdPtr, key, valueDouble);
                    break;
                case string valueString:
                    BidonBannerAdSetExtraDataString(_bannerAdPtr, key, valueString);
                    break;
                case null:
                    BidonBannerAdSetExtraDataNull(_bannerAdPtr, key);
                    break;
            }
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonBannerAdGetExtraData(_bannerAdPtr));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            BidonBannerAdNotifyLoss(_bannerAdPtr, winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonBannerAdNotifyWin(_bannerAdPtr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _instance = null;
            }

            if (_bannerAdPtr != IntPtr.Zero)
            {
                if (BidonBannerAdIsShowing(_bannerAdPtr)) BidonBannerAdHide(_bannerAdPtr);
                BidonBannerAdDestroy(_bannerAdPtr);
                _bannerAdPtr = IntPtr.Zero;
            }
            if (_bannerDelegatePtr != IntPtr.Zero)
            {
                BidonBannerAdDelegateDestroy(_bannerDelegatePtr);
                _bannerDelegatePtr = IntPtr.Zero;
            }
        }
    }
}
#endif
