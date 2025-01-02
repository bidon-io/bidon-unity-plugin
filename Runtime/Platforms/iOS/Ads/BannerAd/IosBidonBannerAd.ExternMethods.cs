#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal partial class IosBidonBannerAd
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDelegateCreate")]
        private static extern IntPtr BidonBannerAdDelegateCreate(AdLoadedCallback onAdLoaded,
                                                                 AdLoadFailedCallback onAdLoadFailed,
                                                                 AdShownCallback onAdShown,
                                                                 AdShowFailedCallback onAdShowFailed,
                                                                 AdClickedCallback onAdClicked,
                                                                 AdExpiredCallback onAdExpired,
                                                                 AdRevenueReceivedCallback onAdRevenueReceived);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdCreate")]
        private static extern IntPtr BidonBannerAdCreate(string auctionKey, IntPtr delegatePtr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetFormat")]
        private static extern void BidonBannerAdSetFormat(IntPtr ptr, BidonBannerFormat format);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdGetFormat")]
        private static extern int BidonBannerAdGetFormat(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdGetSize")]
        private static extern IntPtr BidonBannerAdGetSize(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetPredefinedPosition")]
        private static extern void BidonBannerAdSetPredefinedPosition(IntPtr ptr, BidonBannerPosition position);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdSetCustomPositionAndRotation")]
        private static extern void BidonBannerAdSetCustomPositionAndRotation(IntPtr ptr, int offsetX, int offsetY,
            int angle, float anchorX, float anchorY);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdLoad")]
        private static extern void BidonBannerAdLoad(IntPtr ptr, double priceFloor);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdIsReady")]
        private static extern bool BidonBannerAdIsReady(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdShow")]
        private static extern void BidonBannerAdShow(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdIsShowing")]
        private static extern bool BidonBannerAdIsShowing(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdHide")]
        private static extern void BidonBannerAdHide(IntPtr ptr);

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

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdGetExtraData")]
        private static extern string BidonBannerAdGetExtraData(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdNotifyLoss")]
        private static extern void BidonBannerAdNotifyLoss(IntPtr ptr, string winnerDemandId, double price);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdNotifyWin")]
        private static extern void BidonBannerAdNotifyWin(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDestroy")]
        private static extern void BidonBannerAdDestroy(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginBannerAdDelegateDestroy")]
        private static extern void BidonBannerAdDelegateDestroy(IntPtr delegatePtr);
    }
}
#endif
