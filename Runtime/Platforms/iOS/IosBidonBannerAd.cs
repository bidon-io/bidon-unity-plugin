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
    internal class IosBidonBannerAd : IBidonBannerAd
    {
        private static IosBidonBannerAd _instance;

        private IntPtr _bannerAdPtr;
        private IntPtr _bannerDelegatePtr;

        private bool _disposed;

        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdLoadFailedCallback(int cause);
        private delegate void AdShownCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(int cause);
        private delegate void AdClickedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdExpiredCallback(IntPtr iosBidonAdPtr);
        private delegate void AdRevenueReceivedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr);

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;
        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDelegateCreate")]
        private static extern IntPtr BidonBannerAdDelegateCreate(AdLoadedCallback onAdLoaded,
                                                                 AdLoadFailedCallback onAdLoadFailed,
                                                                 AdShownCallback onAdShown,
                                                                 AdShowFailedCallback onAdShowFailed,
                                                                 AdClickedCallback onAdClicked,
                                                                 AdExpiredCallback onAdExpired,
                                                                 AdRevenueReceivedCallback onAdRevenueReceived);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdCreate")]
        private static extern IntPtr BidonBannerAdCreate(IntPtr delegatePtr);

        internal IosBidonBannerAd()
        {
            _instance = this;

            _bannerDelegatePtr = BidonBannerAdDelegateCreate(AdLoaded,
                                                             AdLoadFailed,
                                                             AdShown,
                                                             AdShowFailed,
                                                             AdClicked,
                                                             AdExpired,
                                                             AdRevenueReceived);
            _bannerAdPtr = BidonBannerAdCreate(_bannerDelegatePtr);
        }

        ~IosBidonBannerAd() => Dispose(false);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetFormat")]
        private static extern void BidonBannerAdSetFormat(IntPtr ptr, BidonBannerFormat format);

        public void SetFormat(BidonBannerFormat format)
        {
            if (IsDisposed()) return;
            BidonBannerAdSetFormat(_bannerAdPtr, format);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdGetFormat")]
        private static extern BidonBannerFormat BidonBannerAdGetFormat(IntPtr ptr);

        public BidonBannerFormat GetFormat()
        {
            if (IsDisposed()) return BidonBannerFormat.Banner;
            return BidonBannerAdGetFormat(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetPredefinedPosition")]
        private static extern void BidonBannerAdSetPredefinedPosition(IntPtr ptr, BidonBannerPosition position);

        public void SetPredefinedPosition(BidonBannerPosition position)
        {
            if (IsDisposed()) return;
            BidonBannerAdSetPredefinedPosition(_bannerAdPtr, position);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetCustomPositionAndRotation")]
        private static extern void BidonBannerAdSetCustomPositionAndRotation(IntPtr ptr, int offsetX, int offsetY,
            int angle, float anchorX, float anchorY);

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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdLoad")]
        private static extern void BidonBannerAdLoad(IntPtr ptr, double priceFloor);

        public void Load(double priceFloor)
        {
            if (IsDisposed()) return;
            BidonBannerAdLoad(_bannerAdPtr, priceFloor);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdIsReady")]
        private static extern bool BidonBannerAdIsReady(IntPtr ptr);

        public bool IsReady()
        {
            if (IsDisposed()) return false;
            return BidonBannerAdIsReady(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdShow")]
        private static extern void BidonBannerAdShow(IntPtr ptr);

        public void Show()
        {
            if (IsDisposed()) return;
            BidonBannerAdShow(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdIsShowing")]
        private static extern bool BidonBannerAdIsShowing(IntPtr ptr);

        public bool IsShowing()
        {
            if (IsDisposed()) return false;
            return BidonBannerAdIsShowing(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdHide")]
        private static extern void BidonBannerAdHide(IntPtr ptr);

        public void Hide()
        {
            if (IsDisposed()) return;
            BidonBannerAdHide(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataBool")]
        private static extern void BidonBannerAdSetExtraDataBool(IntPtr ptr, string key, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataInt")]
        private static extern void BidonBannerAdSetExtraDataInt(IntPtr ptr, string key, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataLong")]
        private static extern void BidonBannerAdSetExtraDataLong(IntPtr ptr, string key, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataFloat")]
        private static extern void BidonBannerAdSetExtraDataFloat(IntPtr ptr, string key, float value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataDouble")]
        private static extern void BidonBannerAdSetExtraDataDouble(IntPtr ptr, string key, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataString")]
        private static extern void BidonBannerAdSetExtraDataString(IntPtr ptr, string key, string value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetExtraDataNull")]
        private static extern void BidonBannerAdSetExtraDataNull(IntPtr ptr, string key);

        public void SetExtraData(string key, object value)
        {
            if (IsDisposed()) return;
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdGetExtraData")]
        private static extern string BidonBannerAdGetExtraData(IntPtr ptr);

        public IDictionary<string, object> GetExtraData()
        {
            if (IsDisposed()) return new Dictionary<string, object>();
            return IosBidonHelper.GetDictionaryFromJsonString(BidonBannerAdGetExtraData(_bannerAdPtr));
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdNotifyLoss")]
        private static extern void BidonBannerAdNotifyLoss(IntPtr ptr, string winnerDemandId, double ecpm);

        public void NotifyLoss(string winnerDemandId, double ecpm)
        {
            if (IsDisposed()) return;
            BidonBannerAdNotifyLoss(_bannerAdPtr, winnerDemandId, ecpm);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdNotifyWin")]
        private static extern void BidonBannerAdNotifyWin(IntPtr ptr);

        public void NotifyWin()
        {
            if (IsDisposed()) return;
            BidonBannerAdNotifyWin(_bannerAdPtr);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDestroy")]
        private static extern void BidonBannerAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDelegateDestroy")]
        private static extern void BidonBannerAdDelegateDestroy(IntPtr delegatePtr);

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
