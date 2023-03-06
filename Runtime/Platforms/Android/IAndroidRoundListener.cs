#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidRoundListener
    {
        void onRoundStarted(string roundId, double priceFloor);
        void onRoundSucceed(string roundId, AndroidJavaObject roundResults);
        void onRoundFailed(string roundId, AndroidJavaObject cause);
    }
}
#endif
