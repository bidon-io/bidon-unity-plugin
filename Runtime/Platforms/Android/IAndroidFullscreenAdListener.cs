#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidFullscreenAdListener
    {
        void onAdShowFailed(AndroidJavaObject cause);
        void onAdClosed(AndroidJavaObject ad);
    }
}
#endif
