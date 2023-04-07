#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
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

        private delegate void AdLoadFailedCallback(int cause);
        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(IntPtr iosBidonImpressionPtr, int cause);
        private delegate void AdShownCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClosedCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClickedCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdRevenueReceivedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr);
        private delegate void UserRewardedCallback(IntPtr iosBidonRewardPtr, IntPtr iosBidonImpressionPtr);

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
        private static extern IntPtr BidonCreateRewardedDelegate(AdLoadFailedCallback onAdLoadFailed,
                                                                 AdLoadedCallback onAdLoaded,
                                                                 AdShowFailedCallback onAdShowFailed,
                                                                 AdShownCallback onAdShown,
                                                                 AdClosedCallback onAdClosed,
                                                                 AdClickedCallback onAdClicked,
                                                                 AdRevenueReceivedCallback onAdRevenueReceived,
                                                                 UserRewardedCallback onUserRewarded);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateRewarded")]
        private static extern IntPtr BidonCreateRewarded(IntPtr delegatePtr);

        internal IosBidonRewardedAd()
        {
            _instance = this;

            _rewardedDelegatePtr = BidonCreateRewardedDelegate(AdLoadFailed,
                                                               AdLoaded,
                                                               AdShowFailed,
                                                               AdShown,
                                                               AdClosed,
                                                               AdClicked,
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

        [MonoPInvokeCallback(typeof(AdLoadFailedCallback))]
        private static void AdLoadFailed(int cause)
        {
            Debug.Log($"[BDNDEBUG] [Rewarded] AdLoadFailed: cause: {cause}");
            SyncContextHelper.Post(state => _instance.OnAdLoadFailed?.Invoke(_instance, new BidonAdLoadFailedEventArgs(BidonError.Unspecified)));
        }

        [MonoPInvokeCallback(typeof(AdLoadedCallback))]
        private static void AdLoaded(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }
            Debug.Log($"[BDNDEBUG] [Rewarded] AdLoaded: ad: {ad?.ToJsonString(false) ?? "null"}");

            SyncContextHelper.Post(state => _instance.OnAdLoaded?.Invoke(_instance, new BidonAdLoadedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdShowFailedCallback))]
        private static void AdShowFailed(IntPtr iosBidonImpressionPtr, int cause)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Rewarded] AdShowFailed: imp: {impression.ImpressionId}, cause: {cause}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Rewarded] AdShowFailed: impression data is null, cause: {cause}");
            }

            SyncContextHelper.Post(state => _instance.OnAdShowFailed?.Invoke(_instance, new BidonAdShowFailedEventArgs(BidonError.Unspecified)));
        }

        [MonoPInvokeCallback(typeof(AdShownCallback))]
        private static void AdShown(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Rewarded] AdShown: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Rewarded] AdShown: impression data is null");
            }

            SyncContextHelper.Post(state => _instance.OnAdShown?.Invoke(_instance, new BidonAdShownEventArgs(null)));
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void AdClosed(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Rewarded] AdClosed: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Rewarded] AdClosed: impression data is null");
            }

            SyncContextHelper.Post(state => _instance.OnAdClosed?.Invoke(_instance, new BidonAdClosedEventArgs(null)));
        }

        [MonoPInvokeCallback(typeof(AdClickedCallback))]
        private static void AdClicked(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Rewarded] AdClicked: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Rewarded] AdClicked: impression data is null");
            }

            SyncContextHelper.Post(state => _instance.OnAdClicked?.Invoke(_instance, new BidonAdClickedEventArgs(null)));
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

            Debug.Log($"[BDNDEBUG] [Rewarded] AdRevenueReceived: ad: {ad?.ToJsonString(false) ?? "null"}, adValue: {adValue?.ToJsonString(false) ?? "null"}");

            SyncContextHelper.Post(state => _instance.OnAdRevenueReceived?.Invoke(_instance, new BidonAdRevenueReceivedEventArgs(ad, adValue)));
        }

        [MonoPInvokeCallback(typeof(UserRewardedCallback))]
        private static void UserRewarded(IntPtr iosBidonRewardPtr, IntPtr iosBidonImpressionPtr)
        {
            BidonReward reward = null;
            if (iosBidonRewardPtr != IntPtr.Zero)
            {
                var iosBidonReward = Marshal.PtrToStructure<IosBidonReward>(iosBidonRewardPtr);
                reward = iosBidonReward.ToBidonReward();
            }

            Debug.Log($"[BDNDEBUG] [Rewarded] UserRewarded: reward: {reward?.ToJsonString(false) ?? "null"}");

            SyncContextHelper.Post(state => _instance.OnUserRewarded?.Invoke(_instance, new BidonUserRewardedEventArgs(null, reward)));
        }
    }
}
#endif
