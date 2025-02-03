#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidUserRewardListener
    {
        void onUserRewarded(AndroidJavaObject ad, AndroidJavaObject reward);
    }
}
#endif
