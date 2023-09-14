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
var bidonInter = new BidonInterstitialAd();
bidonInter.OnAdRevenueReceived += (sender, args) =>
{
    double revenue = args.AdValue.AdRevenue;
    string currency = args.AdValue.CurrencyCode;
    string network = args.Ad.NetworkName;
    string adUnitId = args.Ad.AdUnitId;
};
```

## Sending Extra Data

Note that the value has to be one of the following types: `bool`, `char`, `int`, `long`, `float`, `double`, `string`.
Passing `null` as a value will remove existing `KeyValuePair` from dictionary.

```c#
BidonSdk.Instance.SetExtraData("sdk_extra_bool_key", 0.423d);
```

## Users' Segmentation

You can pass some additional data to Bidon for segmentation purposes. Below you will find predefined arguments.

```c#
BidonSdk.Instance.Segment.Age = 42;
BidonSdk.Instance.Segment.Gender = BidonUserGender.Male;
BidonSdk.Instance.Segment.Level = 11;
BidonSdk.Instance.Segment.TotalInAppsAmount = Double.MaxValue;
BidonSdk.Instance.Segment.IsPaying = true;
```

But you can also send custom attributes. Allowed value types are `bool`, `int`, `long`, `double`, `string`.
Passing `null` as a value will remove existing `KeyValuePair` from dictionary.

```c#
BidonSdk.Instance.Segment.SetCustomAttribute("some_attr_key", "some_attr_value");
```

## GDPR / COPPA Regulations

Read and write consent related data as follows:

```c#
BidonSdk.Instance.Regulation.GdprConsentString = "gdpr_consent_string";
BidonSdk.Instance.Regulation.UsPrivacyString = "us_privacy_string";
BidonSdk.Instance.Regulation.GdprConsentStatus = BidonGdprConsentStatus.Given;
BidonSdk.Instance.Regulation.CoppaApplicabilityStatus = BidonCoppaApplicabilityStatus.Yes;

Debug.Log($"{BidonSdk.Instance.Regulation.GdprConsentString}");
Debug.Log($"{BidonSdk.Instance.Regulation.UsPrivacyString}");
Debug.Log($"{BidonSdk.Instance.Regulation.GdprConsentStatus}");
Debug.Log($"{BidonSdk.Instance.Regulation.CoppaApplicabilityStatus}");
```

## SDK Test Mode

```c#
BidonSdk.Instance.SetTestMode(false);
bool isEnabled = BidonSdk.Instance.IsTestModeEnabled();
```
