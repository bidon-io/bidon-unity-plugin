#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

namespace Bidon.Mediation
{
    internal interface IAndroidInterstitialAdListener :
        IAndroidAdListener,
        IAndroidFullscreenAdListener,
        IAndroidAdRevenueListener
    { }
}
#endif
