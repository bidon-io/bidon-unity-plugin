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
    internal class IosBidonInterstitialAd : IBidonInterstitialAd
    {
        private static IosBidonInterstitialAd _instance;

        private IntPtr _interstitialAdPtr;
        private IntPtr _interstitialDelegatePtr;

        private bool _disposed;

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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdDelegateCreate")]
        private static extern IntPtr BidonInterstitialAdDelegateCreate(AdLoadedCallback onAdLoaded,
                                                                       AdLoadFailedCallback onAdLoadFailed,
                                                                       AdShownCallback onAdShown,
                                                                       AdShowFailedCallback onAdShowFailed,
                                                                       AdClickedCallback onAdClicked,
                                                                       AdClosedCallback onAdClosed,
                                                                       AdExpiredCallback onAdExpired,
                                                                       AdRevenueReceivedCallback onAdRevenueReceived);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdCreate")]
        private static extern IntPtr BidonInterstitialAdCreate(string auctionKey, IntPtr delegatePtr);

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

        ~IosBidonInterstitialAd() => Dispose(false);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdLoad")]
        private static extern void BidonInterstitialAdLoad(IntPtr ptr, double priceFloor);

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonInterstitialAdLoad(_interstitialAdPtr, priceFloor);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdIsReady")]
        private static extern bool BidonInterstitialAdIsReady(IntPtr ptr);

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonInterstitialAdIsReady(_interstitialAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdShow")]
        private static extern void BidonInterstitialAdShow(IntPtr ptr);

        public void Show()
        {
            if (IsDisposed()) return;
            BidonInterstitialAdShow(_interstitialAdPtr);
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
            if (IsDisposed()) return;
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

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonInterstitialAdGetExtraData(_interstitialAdPtr));
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyLoss")]
        private static extern void BidonInterstitialAdNotifyLoss(IntPtr ptr, string winnerDemandId, double ecpm);

        public void NotifyLoss(string winnerDemandId, double ecpm)
        {
            if (IsDisposed()) return;
            BidonInterstitialAdNotifyLoss(_interstitialAdPtr, winnerDemandId, ecpm);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyWin")]
        private static extern void BidonInterstitialAdNotifyWin(IntPtr ptr);

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonInterstitialAdNotifyWin(_interstitialAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdDestroy")]
        private static extern void BidonInterstitialAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdDelegateDestroy")]
        private static extern void BidonInterstitialAdDelegateDestroy(IntPtr delegatePtr);

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

            _disposed = true;
        }

        private bool IsDisposed()
        {
            if (!_disposed) return false;
            Debug.LogError($"[BidonPlugin] {GetType().FullName} instance is disposed. Calling any methods on this instance is not allowed.");
            return true;
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

            SyncContextHelper.Post(state => _instance?.OnAdLoaded?.Invoke(_instance, new BidonAdLoadedEventArgs(ad)));
        }

        [MonoPInvokeCallback(typeof(AdLoadFailedCallback))]
        private static void AdLoadFailed(int cause)
        {
            var error = IosBidonHelper.GetBidonErrorFromInt(cause);
            SyncContextHelper.Post(state => _instance?.OnAdLoadFailed?.Invoke(_instance, new BidonAdLoadFailedEventArgs(error)));
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
    }
}
#endif
