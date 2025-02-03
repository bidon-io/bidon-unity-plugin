#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidAdListener
    {
        void onAdLoaded(AndroidJavaObject ad, AndroidJavaObject auctionInfo);
        void onAdLoadFailed(AndroidJavaObject auctionInfo, AndroidJavaObject cause);
        void onAdShown(AndroidJavaObject ad);
        void onAdShowFailed(AndroidJavaObject cause);
        void onAdClicked(AndroidJavaObject ad);
        void onAdExpired(AndroidJavaObject ad);
    }
}
#endif
