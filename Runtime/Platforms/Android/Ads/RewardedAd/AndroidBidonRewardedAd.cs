#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal partial class AndroidBidonRewardedAd : AndroidBidonAdBase, IBidonRewardedAd
    {
        private AndroidJavaObject _rewardedAdJavaObject;

        internal AndroidBidonRewardedAd(string auctionKey)
        {
            _rewardedAdJavaObject = AndroidBidonFactory.SafeCreateJavaObject("org.bidon.sdk.ads.rewarded.RewardedAd", auctionKey);
            if (_rewardedAdJavaObject == null) return;
            SetListener();
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            _rewardedAdJavaObject.SafeCall("loadAd", ActivityJavaObject, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return _rewardedAdJavaObject.SafeCall<bool>("isReady");
        }

        public void Show()
        {
            if (IsDisposed()) return;
            _rewardedAdJavaObject.SafeCall("showAd", ActivityJavaObject);
        }

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;

            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _rewardedAdJavaObject.SafeCall("addExtra", key, AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_rewardedAdJavaObject.SafeCall<AndroidJavaObject>("getExtras"));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            _rewardedAdJavaObject.SafeCall("notifyLoss", winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            _rewardedAdJavaObject.SafeCall("notifyWin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _rewardedAdJavaObject.SafeCall("destroyAd");
                _rewardedAdJavaObject?.Dispose();
                _rewardedAdJavaObject = null;
            }

            base.Dispose(disposing);
        }
    }
}
#endif
