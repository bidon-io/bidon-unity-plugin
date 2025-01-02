#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonRewardedAd : IosBidonAdBase, IBidonRewardedAd
    {
        private static IosBidonRewardedAd _instance;

        private IntPtr _rewardedAdPtr;
        private IntPtr _rewardedDelegatePtr;

        internal IosBidonRewardedAd(string auctionKey)
        {
            _instance = this;

            _rewardedDelegatePtr = BidonRewardedAdDelegateCreate(AdLoaded,
                                                                 AdLoadFailed,
                                                                 AdShown,
                                                                 AdShowFailed,
                                                                 AdClicked,
                                                                 AdClosed,
                                                                 AdExpired,
                                                                 AdRevenueReceived,
                                                                 UserRewarded);
            _rewardedAdPtr = BidonRewardedAdCreate(auctionKey, _rewardedDelegatePtr);
        }

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonRewardedAdLoad(_rewardedAdPtr, priceFloor);
        }

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonRewardedAdIsReady(_rewardedAdPtr);
        }

        public void Show()
        {
            if (IsDisposed()) return;
            BidonRewardedAdShow(_rewardedAdPtr);
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
                    BidonRewardedAdSetExtraDataBool(_rewardedAdPtr, key, valueBool);
                    break;
                case char valueChar:
                    BidonRewardedAdSetExtraDataString(_rewardedAdPtr, key, valueChar.ToString());
                    break;
                case int valueInt:
                    BidonRewardedAdSetExtraDataInt(_rewardedAdPtr, key, valueInt);
                    break;
                case long valueLong:
                    BidonRewardedAdSetExtraDataLong(_rewardedAdPtr, key, valueLong);
                    break;
                case float valueFloat:
                    BidonRewardedAdSetExtraDataFloat(_rewardedAdPtr, key, valueFloat);
                    break;
                case double valueDouble:
                    BidonRewardedAdSetExtraDataDouble(_rewardedAdPtr, key, valueDouble);
                    break;
                case string valueString:
                    BidonRewardedAdSetExtraDataString(_rewardedAdPtr, key, valueString);
                    break;
                case null:
                    BidonRewardedAdSetExtraDataNull(_rewardedAdPtr, key);
                    break;
            }
        }

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonRewardedAdGetExtraData(_rewardedAdPtr));
        }

        public void NotifyLoss(string winnerDemandId, double price)
        {
            if (IsDisposed()) return;
            BidonRewardedAdNotifyLoss(_rewardedAdPtr, winnerDemandId, price);
        }

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonRewardedAdNotifyWin(_rewardedAdPtr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _instance = null;
            }

            if (_rewardedAdPtr != IntPtr.Zero)
            {
                BidonRewardedAdDestroy(_rewardedAdPtr);
                _rewardedAdPtr = IntPtr.Zero;
            }
            if (_rewardedDelegatePtr != IntPtr.Zero)
            {
                BidonRewardedAdDelegateDestroy(_rewardedDelegatePtr);
                _rewardedDelegatePtr = IntPtr.Zero;
            }
        }
    }
}
#endif
