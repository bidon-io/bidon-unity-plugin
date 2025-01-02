#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonInterstitialAd
    {
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdLoad")]
        private static extern void BidonInterstitialAdLoad(IntPtr ptr, double priceFloor);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdIsReady")]
        private static extern bool BidonInterstitialAdIsReady(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdShow")]
        private static extern void BidonInterstitialAdShow(IntPtr ptr);

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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdGetExtraData")]
        private static extern string BidonInterstitialAdGetExtraData(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyLoss")]
        private static extern void BidonInterstitialAdNotifyLoss(IntPtr ptr, string winnerDemandId, double price);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdNotifyWin")]
        private static extern void BidonInterstitialAdNotifyWin(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdDestroy")]
        private static extern void BidonInterstitialAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInterstitialAdDelegateDestroy")]
        private static extern void BidonInterstitialAdDelegateDestroy(IntPtr delegatePtr);
    }
}
#endif
