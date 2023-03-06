#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidAdRevenueListener
    {
        void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue);
    }
}
#endif
