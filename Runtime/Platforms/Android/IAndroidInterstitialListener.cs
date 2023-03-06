#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidInterstitialListener :
        IAndroidAuctionListener,
        IAndroidRoundListener,
        IAndroidAdListener,
        IAndroidFullscreenAdListener,
        IAndroidAdRevenueListener
    { }
}
#endif
