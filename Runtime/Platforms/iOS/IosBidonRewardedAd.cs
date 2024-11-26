#if UNITY_IOS || BIDON_DEV_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using AOT;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class IosBidonRewardedAd : IBidonRewardedAd
    {
        private static IosBidonRewardedAd _instance;

        private IntPtr _rewardedAdPtr;
        private IntPtr _rewardedDelegatePtr;

        private bool _disposed;

        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAuctionInfoPtr);
        private delegate void AdLoadFailedCallback(IntPtr iosBidonAuctionInfoPtr, int cause);
        private delegate void AdShownCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(int cause);
        private delegate void AdClickedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdClosedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdExpiredCallback(IntPtr iosBidonAdPtr);
        private delegate void AdRevenueReceivedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr);
        private delegate void UserRewardedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonRewardPtr);

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;
        public event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded;

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdDelegateCreate")]
        private static extern IntPtr BidonRewardedAdDelegateCreate(AdLoadedCallback onAdLoaded,
                                                                   AdLoadFailedCallback onAdLoadFailed,
                                                                   AdShownCallback onAdShown,
                                                                   AdShowFailedCallback onAdShowFailed,
                                                                   AdClickedCallback onAdClicked,
                                                                   AdClosedCallback onAdClosed,
                                                                   AdExpiredCallback onAdExpired,
                                                                   AdRevenueReceivedCallback onAdRevenueReceived,
                                                                   UserRewardedCallback onUserRewarded);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdCreate")]
        private static extern IntPtr BidonRewardedAdCreate(string auctionKey, IntPtr delegatePtr);

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

        ~IosBidonRewardedAd() => Dispose(false);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdLoad")]
        private static extern void BidonRewardedAdLoad(IntPtr ptr, double priceFloor);

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonRewardedAdLoad(_rewardedAdPtr, priceFloor);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdIsReady")]
        private static extern bool BidonRewardedAdIsReady(IntPtr ptr);

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonRewardedAdIsReady(_rewardedAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdShow")]
        private static extern void BidonRewardedAdShow(IntPtr ptr);

        public void Show()
        {
            if (IsDisposed()) return;
            BidonRewardedAdShow(_rewardedAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataBool")]
        private static extern void BidonRewardedAdSetExtraDataBool(IntPtr ptr, string key, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataInt")]
        private static extern void BidonRewardedAdSetExtraDataInt(IntPtr ptr, string key, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataLong")]
        private static extern void BidonRewardedAdSetExtraDataLong(IntPtr ptr, string key, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataFloat")]
        private static extern void BidonRewardedAdSetExtraDataFloat(IntPtr ptr, string key, float value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataDouble")]
        private static extern void BidonRewardedAdSetExtraDataDouble(IntPtr ptr, string key, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataString")]
        private static extern void BidonRewardedAdSetExtraDataString(IntPtr ptr, string key, string value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdSetExtraDataNull")]
        private static extern void BidonRewardedAdSetExtraDataNull(IntPtr ptr, string key);

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdGetExtraData")]
        private static extern string BidonRewardedAdGetExtraData(IntPtr ptr);

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonRewardedAdGetExtraData(_rewardedAdPtr));
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyLoss")]
        private static extern void BidonRewardedAdNotifyLoss(IntPtr ptr, string winnerDemandId, double ecpm);

        public void NotifyLoss(string winnerDemandId, double ecpm)
        {
            if (IsDisposed()) return;
            BidonRewardedAdNotifyLoss(_rewardedAdPtr, winnerDemandId, ecpm);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyWin")]
        private static extern void BidonRewardedAdNotifyWin(IntPtr ptr);

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonRewardedAdNotifyWin(_rewardedAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdDestroy")]
        private static extern void BidonRewardedAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdDelegateDestroy")]
        private static extern void BidonRewardedAdDelegateDestroy(IntPtr delegatePtr);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

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

            _disposed = true;
        }

        private bool IsDisposed()
        {
            if (!_disposed) return false;
            Debug.LogError($"[BidonPlugin] {GetType().FullName} instance is disposed. Calling any methods on this instance is not allowed.");
            return true;
        }

        [MonoPInvokeCallback(typeof(AdLoadedCallback))]
        private static void AdLoaded(IntPtr iosBidonAdPtr, IntPtr iosBidonAuctionInfoPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            BidonAuctionInfo auctionInfo = null;
            if (iosBidonAuctionInfoPtr != IntPtr.Zero)
            {
                var iosBidonAuctionInfo = Marshal.PtrToStructure<IosBidonAuctionInfo>(iosBidonAuctionInfoPtr);
                auctionInfo = iosBidonAuctionInfo.ToBidonAuctionInfo();
            }

            SyncContextHelper.Post(state => _instance?.OnAdLoaded?.Invoke(_instance, new BidonAdLoadedEventArgs(ad, auctionInfo)));
        }

        [MonoPInvokeCallback(typeof(AdLoadFailedCallback))]
        private static void AdLoadFailed(IntPtr iosBidonAuctionInfoPtr, int cause)
        {
            BidonAuctionInfo auctionInfo = null;
            if (iosBidonAuctionInfoPtr != IntPtr.Zero)
            {
                var iosBidonAuctionInfo = Marshal.PtrToStructure<IosBidonAuctionInfo>(iosBidonAuctionInfoPtr);
                auctionInfo = iosBidonAuctionInfo.ToBidonAuctionInfo();
            }

            var error = IosBidonHelper.GetBidonErrorFromInt(cause);
            SyncContextHelper.Post(state => _instance?.OnAdLoadFailed?.Invoke(_instance, new BidonAdLoadFailedEventArgs(auctionInfo, error)));
        }

        [MonoPInvokeCallback(typeof(AdShownCallback))]
        private static void AdShown(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            SyncContextHelper.Post(state => _instance?.OnAdShown?.Invoke(_instance, new BidonAdShownEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdShowFailedCallback))]
        private static void AdShowFailed(int cause)
        {
            var error = IosBidonHelper.GetBidonErrorFromInt(cause);
            SyncContextHelper.Post(state => _instance?.OnAdShowFailed?.Invoke(_instance, new BidonAdShowFailedEventArgs(error)));
        }

        [MonoPInvokeCallback(typeof(AdClickedCallback))]
        private static void AdClicked(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            SyncContextHelper.Post(state => _instance?.OnAdClicked?.Invoke(_instance, new BidonAdClickedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void AdClosed(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            SyncContextHelper.Post(state => _instance?.OnAdClosed?.Invoke(_instance, new BidonAdClosedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdExpiredCallback))]
        private static void AdExpired(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            SyncContextHelper.Post(state => _instance?.OnAdExpired?.Invoke(_instance, new BidonAdExpiredEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdRevenueReceivedCallback))]
        private static void AdRevenueReceived(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            BidonAdValue adValue = null;
            if (iosBidonAdRevenuePtr != IntPtr.Zero)
            {
                var iosBidonAdRevenue = Marshal.PtrToStructure<IosBidonAdRevenue>(iosBidonAdRevenuePtr);
                adValue = iosBidonAdRevenue.ToBidonAdValue();
            }

            SyncContextHelper.Post(state => _instance?.OnAdRevenueReceived?.Invoke(_instance, new BidonAdRevenueReceivedEventArgs(ad, adValue)));
        }

        [MonoPInvokeCallback(typeof(UserRewardedCallback))]
        private static void UserRewarded(IntPtr iosBidonAdPtr, IntPtr iosBidonRewardPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            BidonReward reward = null;
            if (iosBidonRewardPtr != IntPtr.Zero)
            {
                var iosBidonReward = Marshal.PtrToStructure<IosBidonReward>(iosBidonRewardPtr);
                reward = iosBidonReward.ToBidonReward();
            }

            SyncContextHelper.Post(state => _instance?.OnUserRewarded?.Invoke(_instance, new BidonUserRewardedEventArgs(ad, reward)));
        }
    }
}
#endif
