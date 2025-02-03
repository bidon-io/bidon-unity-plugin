#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal partial class AndroidBidonBannerAd : AndroidBidonAdBase, IBidonBannerAd
    {
        private AndroidJavaObject _bannerAdJavaObject;

        internal AndroidBidonBannerAd(string auctionKey)
        {
            _bannerAdJavaObject = AndroidBidonFactory.SafeCreateJavaObject("org.bidon.sdk.ads.banner.BannerManager", auctionKey);
            if (_bannerAdJavaObject == null) return;
            SetListener();
        }

        public void SetFormat(BidonBannerFormat format)
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("setBannerFormat", format.ToJavaObject());
        }

        public BidonBannerFormat? GetFormat()
        {
            if (IsDisposed()) return null;
            return _bannerAdJavaObject.SafeCall<AndroidJavaObject>("getBannerFormat").ToBidonBannerFormat();
        }

        public BidonBannerSize GetSize()
        {
            if (IsDisposed()) return null;
            return _bannerAdJavaObject.SafeCall<AndroidJavaObject>("getAdSize").ToBidonBannerSize();
        }

        public void SetPredefinedPosition(BidonBannerPosition position)
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("setPosition", position.ToJavaObject());
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle, Vector2 anchorPoint)
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("setCustomPosition", positionOffset.ToJavaObject(), rotationAngle, anchorPoint.ToJavaObject());
        }

        public void SetCustomPositionAndRotation(Vector2Int positionOffset, int rotationAngle)
        {
            if (IsDisposed()) return;
            SetCustomPositionAndRotation(positionOffset, rotationAngle, new Vector2(0.5f, 0.5f));
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("loadAd", ActivityJavaObject, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return _bannerAdJavaObject.SafeCall<bool>("isReady");
        }

        public void Show()
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("showAd", ActivityJavaObject);
        }

        public bool IsShowing()
        {
            if (IsDisposed()) return false;
            return _bannerAdJavaObject.SafeCall<bool>("isDisplaying");
        }

        public void Hide()
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("hideAd", ActivityJavaObject);
        }

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;

            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _bannerAdJavaObject.SafeCall("addExtra", key, AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_bannerAdJavaObject.SafeCall<AndroidJavaObject>("getExtras"));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("notifyLoss", ActivityJavaObject, winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            _bannerAdJavaObject.SafeCall("notifyWin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bannerAdJavaObject.SafeCall("destroyAd", ActivityJavaObject);
                _bannerAdJavaObject?.Dispose();
                _bannerAdJavaObject = null;
            }

            base.Dispose(disposing);
        }
    }
}
#endif
