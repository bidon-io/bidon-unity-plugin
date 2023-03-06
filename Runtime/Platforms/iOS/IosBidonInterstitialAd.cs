#if UNITY_IOS
using System;
using System.Linq;
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

        private delegate void AuctionStartedCallback();
        private delegate void AuctionFinishedCallback(IntPtr iosBidonAdPtr);

        private delegate void RoundStartedCallback(string roundId, double priceFloor);
        private delegate void RoundFinishedCallback(IntPtr iosBidonAuctionRoundPtr);

        private delegate void BidReceivedCallback(IntPtr iosBidonAdPtr);

        private delegate void AdLoadFailedCallback(int cause);
        private delegate void AdLoadedCallback(IntPtr iosBidonAdPtr);
        private delegate void AdShowFailedCallback(IntPtr iosBidonImpressionPtr, int cause);
        private delegate void AdShownCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClosedCallback(IntPtr iosBidonImpressionPtr);
        private delegate void AdClickedCallback(IntPtr iosBidonImpressionPtr);

        private delegate void AdRevenueReceivedCallback(IntPtr iosBidonAdPtr, IntPtr iosBidonAdRevenuePtr);

        public event EventHandler<BidonAuctionStartedEventArgs> OnAuctionStarted;
        public event EventHandler<BidonAuctionSucceedEventArgs> OnAuctionSucceed;
        public event EventHandler<BidonAuctionFailedEventArgs> OnAuctionFailed;

        public event EventHandler<BidonRoundStartedEventArgs> OnRoundStarted;
        public event EventHandler<BidonRoundSucceedEventArgs> OnRoundSucceed;
        public event EventHandler<BidonRoundFailedEventArgs> OnRoundFailed;

        public event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
        public event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
        public event EventHandler<BidonAdShownEventArgs> OnAdShown;
        public event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
        public event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
        public event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
        public event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

        public event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateInterstitialDelegate")]
        private static extern IntPtr BidonCreateInterstitialDelegate(AuctionStartedCallback onAuctionStarted,
                                                                    AuctionFinishedCallback onAuctionFinished,
                                                                    RoundStartedCallback onRoundStarted,
                                                                    RoundFinishedCallback onRoundFinished,
                                                                    BidReceivedCallback onBidReceived,
                                                                    AdLoadFailedCallback onAdLoadFailed,
                                                                    AdLoadedCallback onAdLoaded,
                                                                    AdShowFailedCallback onAdShowFailed,
                                                                    AdShownCallback onAdShown,
                                                                    AdClosedCallback onAdClosed,
                                                                    AdClickedCallback onAdClicked,
                                                                    AdRevenueReceivedCallback onAdRevenueReceived
                                                                    );

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateInterstitial")]
        private static extern IntPtr BidonCreateInterstitial(string placement, IntPtr delegatePtr);

        internal IosBidonInterstitialAd(string placement)
        {
            _instance = this;

            _interstitialDelegatePtr = BidonCreateInterstitialDelegate(AuctionStarted, 
                                                                    AuctionFinished, 
                                                                    RoundStarted, 
                                                                    RoundFinished, 
                                                                    BidReceived, 
                                                                    AdLoadFailed, 
                                                                    AdLoaded, 
                                                                    AdShowFailed, 
                                                                    AdShown, 
                                                                    AdClosed, 
                                                                    AdClicked, 
                                                                    AdRevenueReceived);
            _interstitialAdPtr = BidonCreateInterstitial(placement, _interstitialDelegatePtr);
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginGetInterstitialPlacementId")]
        private static extern string BidonGetInterstitialPlacementId(IntPtr ptr);

        public string GetPlacementId()
        {
            return BidonGetInterstitialPlacementId(_interstitialAdPtr);
        }

        [MonoPInvokeCallback(typeof(AuctionStartedCallback))]
        private static void AuctionStarted()
        {
            Debug.Log("[BDNDEBUG] [Interstitial] AuctionStarted");
            SyncContextHelper.Post(state => _instance.OnAuctionStarted?.Invoke(_instance, new BidonAuctionStartedEventArgs()));
        }

        [MonoPInvokeCallback(typeof(AuctionFinishedCallback))]
        private static void AuctionFinished(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
                SyncContextHelper.Post(state => _instance.OnAuctionSucceed?.Invoke(_instance, new BidonAuctionSucceedEventArgs(Enumerable.Empty<BidonAuctionResult>())));
            }
            else
            {
                SyncContextHelper.Post(state => _instance.OnAuctionFailed?.Invoke(_instance, new BidonAuctionFailedEventArgs(BidonError.Unspecified)));
            }

            Debug.Log($"[BDNDEBUG] [Interstitial] AuctionFinished: ad: {ad?.ToJsonString(false) ?? "null"}");
        }

        [MonoPInvokeCallback(typeof(RoundStartedCallback))]
        private static void RoundStarted(string roundId, double priceFloor)
        {
            Debug.Log($"[BDNDEBUG] [Interstitial] RoundStarted, id: {roundId}, priceFloor: {priceFloor}");
            SyncContextHelper.Post(state => _instance.OnRoundStarted?.Invoke(_instance, new BidonRoundStartedEventArgs(roundId, priceFloor)));
        }

        [MonoPInvokeCallback(typeof(RoundFinishedCallback))]
        private static void RoundFinished(IntPtr iosBidonAuctionRoundPtr)
        {
            if (iosBidonAuctionRoundPtr != IntPtr.Zero)
            {
                var round = Marshal.PtrToStructure<IosBidonAuctionRound>(iosBidonAuctionRoundPtr);
                Debug.Log($"[BDNDEBUG] [Interstitial] RoundFinished: id: {round.RoundId}, demands: {round.Demands}, timeout: {round.Timeout}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Interstitial] RoundFinished: round data is null");
            }

            // TODO - Not yet possible to detect success or fail based on provided data
            // SyncContextHelper.Post(state => _instance.OnRoundSucceed?.Invoke(_instance, new BidonRoundSucceedEventArgs(round.RoundId, Enumerable.Empty<BidonAuctionResult>())));
            // SyncContextHelper.Post(state => _instance.OnRoundFailed?.Invoke(_instance, new BidonRoundFailedEventArgs(round.RoundId, BidonError.Unspecified)));
        }

        [MonoPInvokeCallback(typeof(BidReceivedCallback))]
        private static void BidReceived(IntPtr iosBidonAdPtr)
        {
            BidonAd ad = null;
            if (iosBidonAdPtr != IntPtr.Zero)
            {
                var iosBidonAd = Marshal.PtrToStructure<IosBidonAd>(iosBidonAdPtr);
                ad = iosBidonAd.ToBidonAd();
            }
            Debug.Log($"[BDNDEBUG] [Interstitial] BidReceived: ad: {ad?.ToJsonString(false) ?? "null"}");
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
