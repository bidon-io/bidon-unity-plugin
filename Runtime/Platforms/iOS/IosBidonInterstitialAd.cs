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
    internal class IosBidonInterstitialAd : IBidonInterstitialAd
    {
        private static IosBidonInterstitialAd _instance;

        private IntPtr _interstitialAdPtr;
        private IntPtr _interstitialDelegatePtr;

        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdLoadFailedCallback(int cause);
        private delegate void AdShownCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(int cause);
        private delegate void AdClickedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdClosedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdExpiredCallback(IntPtr iosBidonAdPtr);
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
        private static extern IntPtr BidonCreateInterstitialDelegate(AdLoadedCallback onAdLoaded,
                                                                     AdLoadFailedCallback onAdLoadFailed,
                                                                     AdShownCallback onAdShown,
                                                                     AdShowFailedCallback onAdShowFailed,
                                                                     AdClickedCallback onAdClicked,
                                                                     AdClosedCallback onAdClosed,
                                                                     AdExpiredCallback onAdExpired,
                                                                     AdRevenueReceivedCallback onAdRevenueReceived);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateInterstitial")]
        private static extern IntPtr BidonCreateInterstitial(IntPtr delegatePtr);

        internal IosBidonInterstitialAd()
        {
            _instance = this;

            _interstitialDelegatePtr = BidonCreateInterstitialDelegate(AdLoaded,
                                                                       AdLoadFailed,
                                                                       AdShown,
                                                                       AdShowFailed,
                                                                       AdClicked,
                                                                       AdClosed,
                                                                       AdExpired,
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataBool")]
        private static extern void BidonInterstitialAdSetExtraDataBool(IntPtr ptr, string key, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataInt")]
        private static extern void BidonInterstitialAdSetExtraDataInt(IntPtr ptr, string key, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataLong")]
        private static extern void BidonInterstitialAdSetExtraDataLong(IntPtr ptr, string key, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataFloat")]
        private static extern void BidonInterstitialAdSetExtraDataFloat(IntPtr ptr, string key, float value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataDouble")]
        private static extern void BidonInterstitialAdSetExtraDataDouble(IntPtr ptr, string key, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataString")]
        private static extern void BidonInterstitialAdSetExtraDataString(IntPtr ptr, string key, string value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdSetExtraDataNull")]
        private static extern void BidonInterstitialAdSetExtraDataNull(IntPtr ptr, string key);

        public void SetExtraData(string key, object value)
        {
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdGetExtraData")]
        private static extern string BidonInterstitialAdGetExtraData(IntPtr ptr);

        public IDictionary<string, object> GetExtraData() =>
            IosBidonHelper.GetDictionaryFromJsonString(BidonInterstitialAdGetExtraData(_interstitialAdPtr));

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyLoss")]
        private static extern void BidonInterstitialAdNotifyLoss(IntPtr ptr, string winnerDemandId, double ecpm);

        public void NotifyLoss(string winnerDemandId, double ecpm) =>
            BidonInterstitialAdNotifyLoss(_interstitialAdPtr, winnerDemandId, ecpm);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyWin")]
        private static extern void BidonInterstitialAdNotifyWin(IntPtr ptr);

        public void NotifyWin() => BidonInterstitialAdNotifyWin(_interstitialAdPtr);

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
    }
}
#endif
