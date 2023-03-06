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
    internal class IosBidonRewardedAd : IBidonRewardedAd
    {
        private static IosBidonRewardedAd _instance;

        private IntPtr _rewardedAdPtr;
        private IntPtr _rewardedDelegatePtr;

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

        private delegate void UserRewardedCallback(IntPtr iosBidonRewardPtr, IntPtr iosBidonImpressionPtr);

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

        public event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded; 

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateRewardedDelegate")]
        private static extern IntPtr BidonCreateRewardedDelegate(AuctionStartedCallback onAuctionStarted,
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
                                                                AdRevenueReceivedCallback onAdRevenueReceived,
                                                                UserRewardedCallback onUserRewarded);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginCreateRewarded")]
        private static extern IntPtr BidonCreateRewarded(string placement, IntPtr delegatePtr);

        internal IosBidonRewardedAd(string placement)
        {
            _instance = this;

            _rewardedDelegatePtr = BidonCreateRewardedDelegate(AuctionStarted, 
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
                                                                    AdRevenueReceived,
                                                                    UserRewarded);
            _rewardedAdPtr = BidonCreateRewarded(placement, _rewardedDelegatePtr);
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginGetRewardedPlacementId")]
        private static extern string BidonGetRewardedPlacementId(IntPtr ptr);

        public string GetPlacementId()
        {
            return BidonGetRewardedPlacementId(_rewardedAdPtr);
        }

        [MonoPInvokeCallback(typeof(AuctionStartedCallback))]
        private static void AuctionStarted()
        {
            Debug.Log("[BDNDEBUG] [Rewarded] AuctionStarted");
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
            Debug.Log($"[BDNDEBUG] [Rewarded] AuctionFinished: ad: {ad?.ToJsonString(false) ?? "null"}");
        }

        [MonoPInvokeCallback(typeof(RoundStartedCallback))]
        private static void RoundStarted(string roundId, double priceFloor)
        {
            Debug.Log($"[BDNDEBUG] [Rewarded] RoundStarted, id: {roundId}, priceFloor: {priceFloor}");
            SyncContextHelper.Post(state => _instance.OnRoundStarted?.Invoke(_instance, new BidonRoundStartedEventArgs(roundId, priceFloor)));
        }

        [MonoPInvokeCallback(typeof(RoundFinishedCallback))]
        private static void RoundFinished(IntPtr iosBidonAuctionRoundPtr)
        {
            if (iosBidonAuctionRoundPtr != IntPtr.Zero)
            {
                var round = Marshal.PtrToStructure<IosBidonAuctionRound>(iosBidonAuctionRoundPtr);
                Debug.Log($"[BDNDEBUG] [Rewarded] RoundFinished: id: {round.RoundId}, demands: {round.Demands}, timeout: {round.Timeout}");
            }
            else
            {
                Debug.Log($"[BDNDEBUG] [Rewarded] RoundFinished: round data is null");
            }

            // TODO - It's not yet possible to detect success or fail based on provided data
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
            Debug.Log($"[BDNDEBUG] [Rewarded] BidReceived: ad: {ad?.ToJsonString(false) ?? "null"}");
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
