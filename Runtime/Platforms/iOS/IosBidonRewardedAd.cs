#if UNITY_IOS || BIDON_DEV_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using AOT;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class IosBidonRewardedAd : IBidonRewardedAd
    {
        private static IosBidonRewardedAd _instance;

        private IntPtr _rewardedAdPtr;
        private IntPtr _rewardedDelegatePtr;

        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdLoadFailedCallback(int cause);
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateRewardedDelegate")]
        private static extern IntPtr BidonCreateRewardedDelegate(AdLoadedCallback onAdLoaded,
                                                                 AdLoadFailedCallback onAdLoadFailed,
                                                                 AdShownCallback onAdShown,
                                                                 AdShowFailedCallback onAdShowFailed,
                                                                 AdClickedCallback onAdClicked,
                                                                 AdClosedCallback onAdClosed,
                                                                 AdExpiredCallback onAdExpired,
                                                                 AdRevenueReceivedCallback onAdRevenueReceived,
                                                                 UserRewardedCallback onUserRewarded);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateRewarded")]
        private static extern IntPtr BidonCreateRewarded(IntPtr delegatePtr);

        internal IosBidonRewardedAd()
        {
            _instance = this;

            _rewardedDelegatePtr = BidonCreateRewardedDelegate(AdLoaded,
                                                               AdLoadFailed,
                                                               AdShown,
                                                               AdShowFailed,
                                                               AdClicked,
                                                               AdClosed,
                                                               AdExpired,
                                                               AdRevenueReceived,
                                                               UserRewarded);
            _rewardedAdPtr = BidonCreateRewarded(_rewardedDelegatePtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginLoadRewarded")]
        private static extern void BidonLoadRewarded(IntPtr ptr, double priceFloor);

        public void Load(double priceFloor)
        {
            BidonLoadRewarded(_rewardedAdPtr, priceFloor);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginIsRewardedReady")]
        private static extern bool BidonIsRewardedReady(IntPtr ptr);

        public bool IsReady()
        {
            return BidonIsRewardedReady(_rewardedAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginShowRewarded")]
        private static extern void BidonShowRewarded(IntPtr ptr);

        public void Show()
        {
            BidonShowRewarded(_rewardedAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginDestroyRewarded")]
        private static extern void BidonDestroyRewarded(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginDestroyRewardedDelegate")]
        private static extern void BidonDestroyRewardedDelegate(IntPtr delegatePtr);

        public void Destroy()
        {
            BidonDestroyRewarded(_rewardedAdPtr);
            BidonDestroyRewardedDelegate(_rewardedDelegatePtr);
            _rewardedAdPtr = IntPtr.Zero;
            _rewardedDelegatePtr = IntPtr.Zero;
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdGetExtraData")]
        private static extern string BidonRewardedAdGetExtraData(IntPtr ptr);

        public IDictionary<string, object> GetExtraData() =>
            IosBidonHelper.GetDictionaryFromJsonString(BidonRewardedAdGetExtraData(_rewardedAdPtr));

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyLoss")]
        private static extern void BidonRewardedAdNotifyLoss(IntPtr ptr, string winnerDemandId, double ecpm);

        public void NotifyLoss(string winnerDemandId, double ecpm) =>
            BidonRewardedAdNotifyLoss(_rewardedAdPtr, winnerDemandId, ecpm);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyWin")]
        private static extern void BidonRewardedAdNotifyWin(IntPtr ptr);

        public void NotifyWin() => BidonRewardedAdNotifyWin(_rewardedAdPtr);

        [MonoPInvokeCallback(typeof(AdLoadedCallback))]
        private static void AdLoaded(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }

            SyncContextHelper.Post(state => _instance.OnAdLoaded?.Invoke(_instance, new BidonAdLoadedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdLoadFailedCallback))]
        private static void AdLoadFailed(int cause)
        {
            var error = IosBidonHelper.GetBidonErrorFromInt(cause);
            SyncContextHelper.Post(state => _instance.OnAdLoadFailed?.Invoke(_instance, new BidonAdLoadFailedEventArgs(error)));
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

            SyncContextHelper.Post(state => _instance.OnAdShown?.Invoke(_instance, new BidonAdShownEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdShowFailedCallback))]
        private static void AdShowFailed(int cause)
        {
            var error = IosBidonHelper.GetBidonErrorFromInt(cause);
            SyncContextHelper.Post(state => _instance.OnAdShowFailed?.Invoke(_instance, new BidonAdShowFailedEventArgs(error)));
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

            SyncContextHelper.Post(state => _instance.OnAdClicked?.Invoke(_instance, new BidonAdClickedEventArgs(ad)));
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

            SyncContextHelper.Post(state => _instance.OnAdClosed?.Invoke(_instance, new BidonAdClosedEventArgs(ad)));
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

            SyncContextHelper.Post(state => _instance.OnAdExpired?.Invoke(_instance, new BidonAdExpiredEventArgs(ad)));
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

            SyncContextHelper.Post(state => _instance.OnAdRevenueReceived?.Invoke(_instance, new BidonAdRevenueReceivedEventArgs(ad, adValue)));
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

            SyncContextHelper.Post(state => _instance.OnUserRewarded?.Invoke(_instance, new BidonUserRewardedEventArgs(ad, reward)));
        }
    }
}
#endif
