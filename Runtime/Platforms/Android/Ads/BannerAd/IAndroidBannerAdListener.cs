#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

namespace Bidon.Mediation
{
    internal interface IAndroidBannerAdListener :
        IAndroidAdListener,
        IAndroidAdRevenueListener
    { }
}
#endif
