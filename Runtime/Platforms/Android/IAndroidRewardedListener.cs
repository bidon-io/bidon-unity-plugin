#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidRewardedListener :
        IAndroidAuctionListener,
        IAndroidRoundListener,
        IAndroidAdListener,
        IAndroidFullscreenAdListener,
        IAndroidAdRevenueListener,
        IAndroidRewardedAdListener
    { }
}
#endif
