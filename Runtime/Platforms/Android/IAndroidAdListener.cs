#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidAdListener
    {
        void onAdLoaded(AndroidJavaObject ad);
        void onAdLoadFailed(AndroidJavaObject cause);
        void onAdShown(AndroidJavaObject ad);
        void onAdClicked(AndroidJavaObject ad);
        void onAdExpired(AndroidJavaObject ad);
    }
}
#endif
