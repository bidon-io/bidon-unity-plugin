#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonRewardedAd
    {
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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdLoad")]
        private static extern void BidonRewardedAdLoad(IntPtr ptr, double priceFloor);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdIsReady")]
        private static extern bool BidonRewardedAdIsReady(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdShow")]
        private static extern void BidonRewardedAdShow(IntPtr ptr);

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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdGetExtraData")]
        private static extern string BidonRewardedAdGetExtraData(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyLoss")]
        private static extern void BidonRewardedAdNotifyLoss(IntPtr ptr, string winnerDemandId, double price);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdNotifyWin")]
        private static extern void BidonRewardedAdNotifyWin(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdDestroy")]
        private static extern void BidonRewardedAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRewardedAdDelegateDestroy")]
        private static extern void BidonRewardedAdDelegateDestroy(IntPtr delegatePtr);
    }
}
#endif
