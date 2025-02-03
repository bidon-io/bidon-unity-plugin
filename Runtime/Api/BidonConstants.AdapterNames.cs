// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    public static partial class BidonConstants
    {
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("ReSharper", "NotAccessedField.Global")]
        [SuppressMessage("ReSharper", "UnassignedReadonlyField")]
        public static class AdapterNames
        {
            public static readonly string Amazon;
            public static readonly string AppLovin;
            public static readonly string BidMachine;
            public static readonly string BigoAds;
            public static readonly string Chartboost;
            public static readonly string DTExchange;
            public static readonly string GoogleAdManager;
            public static readonly string GoogleMobileAds;
            public static readonly string InMobi;
            public static readonly string IronSource;
            public static readonly string MetaAudienceNetwork;
            public static readonly string Mintegral;
            public static readonly string MobileFuse;
            public static readonly string UnityAds;
            public static readonly string VkAds;
            public static readonly string Vungle;
            public static readonly string Yandex;

            static AdapterNames()
            {
#if UNITY_ANDROID
                Amazon = "org.bidon.amazon.AmazonAdapter";
                AppLovin = "org.bidon.applovin.ApplovinAdapter";
                BidMachine = "org.bidon.bidmachine.BidMachineAdapter";
                BigoAds = "org.bidon.bigoads.BigoAdsAdapter";
                Chartboost = "org.bidon.chartboost.ChartboostAdapter";
                DTExchange = "org.bidon.dtexchange.DTExchangeAdapter";
                GoogleAdManager = "org.bidon.gam.GamAdapter";
                GoogleMobileAds = "org.bidon.admob.AdmobAdapter";
                InMobi = "org.bidon.inmobi.InmobiAdapter";
                IronSource = "org.bidon.ironsource.IronSourceAdapter";
                MetaAudienceNetwork = "org.bidon.meta.MetaAudienceAdapter";
                Mintegral = "org.bidon.mintegral.MintegralAdapter";
                MobileFuse = "org.bidon.mobilefuse.MobileFuseAdapter";
                UnityAds = "org.bidon.unityads.UnityAdsAdapter";
                VkAds = "org.bidon.vkads.VkAdsAdapter";
                Vungle = "org.bidon.vungle.VungleAdapter";
                Yandex = "org.bidon.yandex.YandexAdapter";
#elif UNITY_IOS
                Amazon = "BidonAdapterAmazon.AmazonDemandSourceAdapter";
                AppLovin = "BidonAdapterAppLovin.AppLovinDemandSourceAdapter";
                BidMachine = "BidonAdapterBidMachine.BidMachineDemandSourceAdapter";
                BigoAds = "BidonAdapterBigoAds.BigoAdsDemandSourceAdapter";
                Chartboost = "BidonAdapterChartboost.ChartboostDemandSourceAdapter";
                DTExchange = "BidonAdapterDTExchange.DTExchangeDemandSourceAdapter";
                GoogleAdManager = "BidonAdapterGoogleAdManager.GoogleAdManagerDemandSourceAdapter";
                GoogleMobileAds = "BidonAdapterGoogleMobileAds.GoogleMobileAdsDemandSourceAdapter";
                InMobi = "BidonAdapterInMobi.InMobiDemandSourceAdapter";
                IronSource = "BidonAdapterIronSource.IronSourceDemandSourceAdapter";
                MetaAudienceNetwork = "BidonAdapterMetaAudienceNetwork.MetaAudienceNetworkDemandSourceAdapter";
                Mintegral = "BidonAdapterMintegral.MintegralDemandSourceAdapter";
                MobileFuse = "BidonAdapterMobileFuse.MobileFuseDemandSourceAdapter";
                UnityAds = "BidonAdapterUnityAds.UnityAdsDemandSourceAdapter";
                VkAds = "BidonAdapterMyTarget.MyTargetDemandSourceAdapter";
                Vungle = "BidonAdapterVungle.VungleDemandSourceAdapter";
                Yandex = "BidonAdapterYandex.YandexDemandSourceAdapter";
#endif
            }
        }
    }
}
