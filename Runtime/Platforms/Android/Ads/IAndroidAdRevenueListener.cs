#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidAdRevenueListener
    {
        void onRevenuePaid(AndroidJavaObject ad, AndroidJavaObject adValue);
    }
}
#endif
