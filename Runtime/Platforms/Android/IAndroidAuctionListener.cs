#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidAuctionListener
    {
        void onAuctionStarted();
        void onAuctionSuccess(AndroidJavaObject auctionResults);
        void onAuctionFailed(AndroidJavaObject cause);
    }
}
#endif
