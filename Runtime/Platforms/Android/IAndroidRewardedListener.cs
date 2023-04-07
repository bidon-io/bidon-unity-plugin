#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System.Diagnostics.CodeAnalysis;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface IAndroidRewardedListener :
        IAndroidAdListener,
        IAndroidFullscreenAdListener,
        IAndroidAdRevenueListener,
        IAndroidRewardedAdListener
    { }
}
#endif
