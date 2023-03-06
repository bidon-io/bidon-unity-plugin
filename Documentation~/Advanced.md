# Advanced

This page describes advanced features of the Bidon SDK.

## Manually register adapters

You're able to define which adapters will be used by Bidon SDK in the application runtime by yourself. You'll need to use the following code to register adapters instead of `RegisterDefaultAdapters()` before initialize the Bidon SDK.

```c#
#if UNITY_ANDROID
    BidonSdk.Instance.RegisterAdapter("org.bidon.admob.AdmobAdapter");
#elif UNITY_IOS
    BidonSdk.Instance.RegisterAdapter("BidonAdapterAppLovin.AppLovinDemandSourceAdapter");
#endif
```

## Impression-Level Ad Revenue

You're able to receive impression-level ad revenue data for sending to any MMP or analytics platform.

```c#
var bidonInter = new BidonInterstitialAd("PLACEMENT_NAME");
bidonInter.OnAdRevenueReceived += (sender, args) =>
{
    double revenue = args.AdValue.AdRevenue;
    string currency = args.AdValue.CurrencyCode;
    string network = args.Ad.NetworkName;
    string adUnitId = args.Ad.AdUnitId;
};
```
