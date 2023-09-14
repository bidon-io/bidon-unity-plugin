using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Bidon.Mediation;

[SuppressMessage("ReSharper", "CheckNamespace")]
public class BidonDemoScript : MonoBehaviour
{
    [SerializeField] private Button initButton;
    [SerializeField] private Text versionText;

    private BidonInterstitialAd _interstitialAd;
    private BidonRewardedAd _rewardedAd;

    private void Awake()
    {
        Assert.IsNotNull(initButton);
        Assert.IsNotNull(versionText);

        versionText.text = $"version - {BidonSdk.PluginVersion}";
    }

    public void InitBidonSdk()
    {
        BidonSdk.Instance.OnInitializationFinished += (sender, args) =>
        {
            Debug.Log("[BidonPlugin] [Event] [SDK] OnInitializationFinished raised");
            Debug.Log($"[BidonPlugin] Is SDK initialized: {BidonSdk.Instance.IsInitialized()}");
            Debug.Log($"[BidonPlugin] Plugin version: {BidonSdk.PluginVersion}");
            Debug.Log($"[BidonPlugin] SDK version: {BidonSdk.Instance.GetSdkVersion()}");
            Debug.Log($"[BidonPlugin] Current log level: {BidonSdk.Instance.GetLogLevel().ToString()}");
            Debug.Log($"[BidonPlugin] [Segment] Id: {BidonSdk.Instance.Segment.Id}");
#if UNITY_IOS
            Debug.Log($"[BidonPlugin] Base URL: {BidonSdk.Instance.GetBaseUrl()}");
#endif
        };

        BidonSdk.Instance.SetLogLevel(BidonLogLevel.Verbose);
        BidonSdk.Instance.SetBaseUrl("https://b.appbaqend.com");

        BidonSdk.Instance.SetTestMode(false);
        Debug.Log($"[BidonPlugin] Is test mode enabled: {BidonSdk.Instance.IsTestModeEnabled()}");

        BidonSdk.Instance.Regulation.GdprConsentString = "gdpr_consent_string";
        BidonSdk.Instance.Regulation.UsPrivacyString = "us_privacy_string";
        BidonSdk.Instance.Regulation.GdprConsentStatus = BidonGdprConsentStatus.Given;
        BidonSdk.Instance.Regulation.CoppaApplicabilityStatus = BidonCoppaApplicabilityStatus.Yes;
        Debug.Log($"[BidonPlugin] [Regulation] GdprConsentString: {BidonSdk.Instance.Regulation.GdprConsentString}");
        Debug.Log($"[BidonPlugin] [Regulation] UsPrivacyString: {BidonSdk.Instance.Regulation.UsPrivacyString}");
        Debug.Log($"[BidonPlugin] [Regulation] GdprConsentStatus: {BidonSdk.Instance.Regulation.GdprConsentStatus}");
        Debug.Log($"[BidonPlugin] [Regulation] CoppaApplicabilityStatus: {BidonSdk.Instance.Regulation.CoppaApplicabilityStatus}");

        BidonSdk.Instance.SetExtraData("sdk_extra_bool_key", false);
        BidonSdk.Instance.SetExtraData("sdk_extra_char_key", 'v');
        BidonSdk.Instance.SetExtraData("sdk_extra_int_key", 42);
        BidonSdk.Instance.SetExtraData("sdk_extra_long_key", Int64.MaxValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_float_key", Single.MinValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_double_key", Double.MaxValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_string_key", "sdk_extra_string_value");
        BidonSdk.Instance.SetExtraData("sdk_extra_unwanted_key", 11);
        BidonSdk.Instance.SetExtraData("sdk_extra_unwanted_key", null);

        BidonSdk.Instance.Segment.Age = 42;
        BidonSdk.Instance.Segment.Gender = BidonUserGender.Male;
        BidonSdk.Instance.Segment.Level = 11;
        BidonSdk.Instance.Segment.TotalInAppsAmount = Double.MaxValue;
        BidonSdk.Instance.Segment.IsPaying = true;
        Debug.Log($"[BidonPlugin] [Segment] Age: {BidonSdk.Instance.Segment.Age}");
        Debug.Log($"[BidonPlugin] [Segment] Gender: {BidonSdk.Instance.Segment.Gender}");
        Debug.Log($"[BidonPlugin] [Segment] Level: {BidonSdk.Instance.Segment.Level}");
        Debug.Log($"[BidonPlugin] [Segment] InAppsAmount: {BidonSdk.Instance.Segment.TotalInAppsAmount}");
        Debug.Log($"[BidonPlugin] [Segment] IsPaying: {BidonSdk.Instance.Segment.IsPaying}");

        BidonSdk.Instance.Segment.SetCustomAttribute("segment_bool_attr", true);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_int_attr", Int32.MinValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_long_attr", Int64.MaxValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_double_attr", Double.MinValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_string_attr", "segment_string_value");
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unsupported_attr", 's');
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unwanted_attr", false);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unwanted_attr", null);
        string attributes = String.Join(", ",
            BidonSdk.Instance.Segment.CustomAttributes
                .Select(attr => $"{attr.Key}:({attr.Value.GetType()}){attr.Value}")
                .ToArray());
        Debug.Log($"[BidonPlugin] [Segment] Custom Attributes: {attributes}");

#if UNITY_ANDROID
        BidonSdk.Instance.RegisterAdapter("org.bidon.admob.AdmobAdapter");
#elif UNITY_IOS
        BidonSdk.Instance.RegisterAdapter("BidonAdapterAppLovin.AppLovinDemandSourceAdapter");
#endif

        BidonSdk.Instance.RegisterDefaultAdapters();

#if UNITY_EDITOR
        BidonSdk.Instance.Initialize("");
#elif UNITY_ANDROID
        BidonSdk.Instance.Initialize("b1689e101a2555084e08c2ba7375783bde166625bbeae00f");
#elif UNITY_IOS
        BidonSdk.Instance.Initialize("dee74c5129f53fc629a44a690a02296694e3eef99f2d3a5f");
#else
        BidonSdk.Instance.Initialize("");
#endif

        initButton.interactable = false;
    }

    public void CreateInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            Debug.LogWarning($"[BidonPlugin] Destroy the interstitial first");
            return;
        }

        _interstitialAd = new BidonInterstitialAd();

        SubscribeForInterstitialEvents();

        _interstitialAd.SetExtraData("interstitial_bool_key", false);
        _interstitialAd.SetExtraData("interstitial_char_key", 'i');
        _interstitialAd.SetExtraData("interstitial_int_key", Int32.MinValue);
        _interstitialAd.SetExtraData("interstitial_long_key", Int64.MaxValue);
        _interstitialAd.SetExtraData("interstitial_float_key", Single.MinValue);
        _interstitialAd.SetExtraData("interstitial_double_key", Double.MaxValue);
        _interstitialAd.SetExtraData("interstitial_string_key", "interstitial_string_value");
        _interstitialAd.SetExtraData("interstitial_unwanted_key", false);
        _interstitialAd.SetExtraData("interstitial_unwanted_key", null);

        string extraData = String.Join(", ", _interstitialAd.GetExtraData()
            .Select(kvp => $"{kvp.Key}:({kvp.Value.GetType()}){kvp.Value}")
            .ToArray());
        Debug.Log($"[BidonPlugin] [InterstitialAd] Extra data: {extraData}");

        SubscribeForInterstitialEvents();
    }

    public void LoadInterstitialAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the interstitial first");
            return;
        }
        _interstitialAd.Load(0.01d);
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the interstitial first");
            return;
        }

        if (_interstitialAd.IsReady())
        {
            _interstitialAd.NotifyWin();
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("[BidonPlugin] Load the interstitial first");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the interstitial first");
            return;
        }

        UnsubscribeFromInterstitialEvents();
        _interstitialAd.NotifyLoss("some_winner_id", 0.5);
        _interstitialAd.Destroy();
        _interstitialAd = null;
    }

    public void CreateRewardedAd()
    {
        if (_rewardedAd != null)
        {
            Debug.LogWarning($"[BidonPlugin] Destroy the current rewarded first");
            return;
        }

        _rewardedAd = new BidonRewardedAd();

        SubscribeForRewardedEvents();

        _rewardedAd.SetExtraData("rewarded_bool_key", true);
        _rewardedAd.SetExtraData("rewarded_char_key", 'r');
        _rewardedAd.SetExtraData("rewarded_int_key", Int32.MinValue);
        _rewardedAd.SetExtraData("rewarded_long_key", Int64.MaxValue);
        _rewardedAd.SetExtraData("rewarded_float_key", Single.MinValue);
        _rewardedAd.SetExtraData("rewarded_double_key", Double.MaxValue);
        _rewardedAd.SetExtraData("rewarded_string_key", "rewarded_string_value");
        _rewardedAd.SetExtraData("rewarded_unwanted_key", false);
        _rewardedAd.SetExtraData("rewarded_unwanted_key", null);

        string extraData = String.Join(", ", _rewardedAd.GetExtraData()
            .Select(kvp => $"{kvp.Key}:({kvp.Value.GetType()}){kvp.Value}")
            .ToArray());
        Debug.Log($"[BidonPlugin] [RewardedAd] Extra data: {extraData}");
    }

    public void LoadRewardedAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the rewarded first");
            return;
        }
        _rewardedAd.Load(0.02d);
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the rewarded first");
            return;
        }

        if (_rewardedAd.IsReady())
        {
            _rewardedAd.NotifyWin();
            _rewardedAd.Show();
        }
        else
        {
            Debug.LogWarning("[BidonPlugin] Load the rewarded first");
        }
    }

    public void DestroyRewardedAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] Create the rewarded first");
            return;
        }

        UnsubscribeFromRewardedEvents();
        _rewardedAd.NotifyLoss("some_winner_id", 0.9);
        _rewardedAd.Destroy();
        _rewardedAd = null;
    }

    #region Callbacks

    private void SubscribeForInterstitialEvents()
    {
        _interstitialAd.OnAdLoaded += OnInterstitialAdLoaded;
        _interstitialAd.OnAdLoadFailed += OnInterstitialAdLoadFailed;
        _interstitialAd.OnAdShown += OnInterstitialAdShown;
        _interstitialAd.OnAdShowFailed += OnInterstitialAdShowFailed;
        _interstitialAd.OnAdClicked += OnInterstitialAdClicked;
        _interstitialAd.OnAdClosed += OnInterstitialAdClosed;
        _interstitialAd.OnAdExpired += OnInterstitialAdExpired;
        _interstitialAd.OnAdRevenueReceived += OnInterstitialAdRevenueReceived;
    }

    private void UnsubscribeFromInterstitialEvents()
    {
        _interstitialAd.OnAdLoaded -= OnInterstitialAdLoaded;
        _interstitialAd.OnAdLoadFailed -= OnInterstitialAdLoadFailed;
        _interstitialAd.OnAdShown -= OnInterstitialAdShown;
        _interstitialAd.OnAdShowFailed -= OnInterstitialAdShowFailed;
        _interstitialAd.OnAdClicked -= OnInterstitialAdClicked;
        _interstitialAd.OnAdClosed -= OnInterstitialAdClosed;
        _interstitialAd.OnAdExpired -= OnInterstitialAdExpired;
        _interstitialAd.OnAdRevenueReceived -= OnInterstitialAdRevenueReceived;
    }

    private void SubscribeForRewardedEvents()
    {
        _rewardedAd.OnAdLoaded += OnRewardedAdLoaded;
        _rewardedAd.OnAdLoadFailed += OnRewardedAdLoadFailed;
        _rewardedAd.OnAdShown += OnRewardedAdShown;
        _rewardedAd.OnAdShowFailed += OnRewardedAdShowFailed;
        _rewardedAd.OnAdClicked += OnRewardedAdClicked;
        _rewardedAd.OnAdClosed += OnRewardedAdClosed;
        _rewardedAd.OnAdExpired += OnRewardedAdExpired;
        _rewardedAd.OnAdRevenueReceived += OnRewardedAdRevenueReceived;
        _rewardedAd.OnUserRewarded += OnRewardedUserRewarded;
    }

    private void UnsubscribeFromRewardedEvents()
    {
        _rewardedAd.OnAdLoaded -= OnRewardedAdLoaded;
        _rewardedAd.OnAdLoadFailed -= OnRewardedAdLoadFailed;
        _rewardedAd.OnAdShown -= OnRewardedAdShown;
        _rewardedAd.OnAdShowFailed -= OnRewardedAdShowFailed;
        _rewardedAd.OnAdClicked -= OnRewardedAdClicked;
        _rewardedAd.OnAdClosed -= OnRewardedAdClosed;
        _rewardedAd.OnAdExpired -= OnRewardedAdExpired;
        _rewardedAd.OnAdRevenueReceived -= OnRewardedAdRevenueReceived;
        _rewardedAd.OnUserRewarded -= OnRewardedUserRewarded;
    }

    private void OnInterstitialAdLoaded(object sender, BidonAdLoadedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdLoaded raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnInterstitialAdLoadFailed(object sender, BidonAdLoadFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdLoadFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnInterstitialAdShown(object sender, BidonAdShownEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdShown raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnInterstitialAdShowFailed(object sender, BidonAdShowFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdShowFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnInterstitialAdClicked(object sender, BidonAdClickedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdClicked raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnInterstitialAdClosed(object sender, BidonAdClosedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdClosed raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnInterstitialAdExpired(object sender, BidonAdExpiredEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdExpired raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnInterstitialAdRevenueReceived(object sender, BidonAdRevenueReceivedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAdRevenueReceived raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}, AdValue: {args.AdValue?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdLoaded(object sender, BidonAdLoadedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdLoaded raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdLoadFailed(object sender, BidonAdLoadFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdLoadFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnRewardedAdShown(object sender, BidonAdShownEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdShown raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdShowFailed(object sender, BidonAdShowFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdShowFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnRewardedAdClicked(object sender, BidonAdClickedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdClicked raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdClosed(object sender, BidonAdClosedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdClosed raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdExpired(object sender, BidonAdExpiredEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdExpired raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedAdRevenueReceived(object sender, BidonAdRevenueReceivedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAdRevenueReceived raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}, AdValue: {args.AdValue?.ToJsonString(true) ?? "null"}");
    }

    private void OnRewardedUserRewarded(object sender, BidonUserRewardedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnUserRewarded raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}, Reward: {args.Reward?.ToJsonString(true) ?? "null"}");
    }

    #endregion
}
