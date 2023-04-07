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
    internal class IosBidonInterstitialAd : IBidonInterstitialAd
    {
        private static IosBidonInterstitialAd _instance;

        private IntPtr _interstitialAdPtr;
        private IntPtr _interstitialDelegatePtr;

        private delegate void AdLoadFailedCallback(int cause);
        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(IntPtr iosBidonImpressionPtr, int cause);
        private delegate void AdShownCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClosedCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClickedCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdRevenueReceivedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr);

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateInterstitialDelegate")]
        private static extern IntPtr BidonCreateInterstitialDelegate(AdLoadFailedCallback onAdLoadFailed,
                                                                     AdLoadedCallback onAdLoaded,
                                                                     AdShowFailedCallback onAdShowFailed,
                                                                     AdShownCallback onAdShown,
                                                                     AdClosedCallback onAdClosed,
                                                                     AdClickedCallback onAdClicked,
                                                                     AdRevenueReceivedCallback onAdRevenueReceived);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateInterstitial")]
        private static extern IntPtr BidonCreateInterstitial(IntPtr delegatePtr);

        internal IosBidonInterstitialAd()
        {
            _instance = this;

            _interstitialDelegatePtr = BidonCreateInterstitialDelegate(AdLoadFailed,
                                                                       AdLoaded,
                                                                       AdShowFailed,
                                                                       AdShown,
                                                                       AdClosed,
                                                                       AdClicked,
                                                                       AdRevenueReceived);
            _interstitialAdPtr = BidonCreateInterstitial(_interstitialDelegatePtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginLoadInterstitial")]
        private static extern void BidonLoadInterstitial(IntPtr ptr, double priceFloor);

        public void Load(double priceFloor)
        {
            BidonLoadInterstitial(_interstitialAdPtr, priceFloor);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginIsInterstitialReady")]
        private static extern bool BidonIsInterstitialReady(IntPtr ptr);

        public bool IsReady()
        {
            return BidonIsInterstitialReady(_interstitialAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginShowInterstitial")]
        private static extern void BidonShowInterstitial(IntPtr ptr);

        public void Show()
        {
            BidonShowInterstitial(_interstitialAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginDestroyInterstitial")]
        private static extern void BidonDestroyInterstitial(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginDestroyInterstitialDelegate")]
        private static extern void BidonDestroyInterstitialDelegate(IntPtr delegatePtr);

        public void Destroy()
        {
            BidonDestroyInterstitial(_interstitialAdPtr);
            BidonDestroyInterstitialDelegate(_interstitialDelegatePtr);
            _interstitialAdPtr = IntPtr.Zero;
            _interstitialDelegatePtr = IntPtr.Zero;
        }

        [MonoPInvokeCallback(typeof(AdLoadFailedCallback))]
        private static void AdLoadFailed(int cause)
        {
            Debug.Log($"[BDNDEBUG] [Interstitial] AdLoadFailed: cause: {cause}");
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
            Debug.Log($"[BDNDEBUG] [Interstitial] AdLoaded: ad: {ad?.ToJsonString(false) ?? "null"}");

            SyncContextHelper.Post(state => _instance.OnAdLoaded?.Invoke(_instance, new BidonAdLoadedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdShowFailedCallback))]
        private static void AdShowFailed(IntPtr iosBidonImpressionPtr, int cause)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Interstitial] AdShowFailed: imp: {impression.ImpressionId}, cause: {cause}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Interstitial] AdShowFailed: impression data is null, cause: {cause}");
            }

            SyncContextHelper.Post(state => _instance.OnAdShowFailed?.Invoke(_instance, new BidonAdShowFailedEventArgs(BidonError.Unspecified)));
        }

        [MonoPInvokeCallback(typeof(AdShownCallback))]
        private static void AdShown(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Interstitial] AdShown: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Interstitial] AdShown: impression data is null");
            }

            SyncContextHelper.Post(state => _instance.OnAdShown?.Invoke(_instance, new BidonAdShownEventArgs(null)));
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void AdClosed(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Interstitial] AdClosed: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Interstitial] AdClosed: impression data is null");
            }

            SyncContextHelper.Post(state => _instance.OnAdClosed?.Invoke(_instance, new BidonAdClosedEventArgs(null)));
        }

        [MonoPInvokeCallback(typeof(AdClickedCallback))]
        private static void AdClicked(IntPtr iosBidonImpressionPtr)
        {
            if (iosBidonImpressionPtr != IntPtr.Zero)
            {
                var impression = Marshal.PtrToStructure<IosBidonImpression>(iosBidonImpressionPtr);
                Debug.Log($"[BDNDEBUG] [Interstitial] AdClicked: imp: {impression.ImpressionId}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Interstitial] AdClicked: impression data is null");
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

            Debug.Log($"[BDNDEBUG] [Interstitial] AdRevenueReceived: ad: {ad?.ToJsonString(false) ?? "null"}, adValue: {adValue?.ToJsonString(false) ?? "null"}");

            SyncContextHelper.Post(state => _instance.OnAdRevenueReceived?.Invoke(_instance, new BidonAdRevenueReceivedEventArgs(ad, adValue)));
        }
    }
}
#endif
