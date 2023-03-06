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
#if UNITY_IOS
            Debug.Log($"[BidonPlugin] Base URL: {BidonSdk.Instance.GetBaseUrl()}");
#endif
        };

        BidonSdk.Instance.SetLogLevel(BidonLogLevel.Verbose);
        BidonSdk.Instance.SetBaseUrl("https://b.appbaqend.com");
        BidonSdk.Instance.RegisterDefaultAdapters();

#if UNITY_ANDROID
        BidonSdk.Instance.RegisterAdapter("org.bidon.admob.AdmobAdapter");
#elif UNITY_IOS
        BidonSdk.Instance.RegisterAdapter("BidonAdapterAppLovin.AppLovinDemandSourceAdapter");
#endif

#if UNITY_EDITOR
        BidonSdk.Instance.Initialize("");
#elif UNITY_ANDROID
        BidonSdk.Instance.Initialize("b1689e101a2555084e08c2ba7375783bde166625bbeae00f");
#elif UNITY_IOS
        BidonSdk.Instance.Initialize("3c53cae2cd969ecd82910e1f5610a3df24ea8b4b3ca52247");
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

        _interstitialAd = new BidonInterstitialAd("level_finished");
        Debug.Log($"[BidonPlugin] [Interstitial] placementId: {_interstitialAd.GetPlacementId()}");

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

        _rewardedAd = new BidonRewardedAd("bonus_requested");
        Debug.Log($"[BidonPlugin] [Rewarded] placementId: {_rewardedAd.GetPlacementId()}");

        SubscribeForRewardedEvents();
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
        _rewardedAd.Destroy();
        _rewardedAd = null;
    }

    #region Callbacks

    private void SubscribeForInterstitialEvents()
    {
        _interstitialAd.OnAuctionStarted += OnInterstitialAuctionStarted;
        _interstitialAd.OnAuctionSucceed += OnInterstitialAuctionSucceed;
        _interstitialAd.OnAuctionFailed += OnInterstitialAuctionFailed;
        _interstitialAd.OnRoundStarted += OnInterstitialRoundStarted;
        _interstitialAd.OnRoundSucceed += OnInterstitialRoundSucceed;
        _interstitialAd.OnRoundFailed += OnInterstitialRoundFailed;
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
        _interstitialAd.OnAuctionStarted -= OnInterstitialAuctionStarted;
        _interstitialAd.OnAuctionSucceed -= OnInterstitialAuctionSucceed;
        _interstitialAd.OnAuctionFailed -= OnInterstitialAuctionFailed;
        _interstitialAd.OnRoundStarted -= OnInterstitialRoundStarted;
        _interstitialAd.OnRoundSucceed -= OnInterstitialRoundSucceed;
        _interstitialAd.OnRoundFailed -= OnInterstitialRoundFailed;
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
        _rewardedAd.OnAuctionStarted += OnRewardedAuctionStarted;
        _rewardedAd.OnAuctionSucceed += OnRewardedAuctionSucceed;
        _rewardedAd.OnAuctionFailed += OnRewardedAuctionFailed;
        _rewardedAd.OnRoundStarted += OnRewardedRoundStarted;
        _rewardedAd.OnRoundSucceed += OnRewardedRoundSucceed;
        _rewardedAd.OnRoundFailed += OnRewardedRoundFailed;
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
        _rewardedAd.OnAuctionStarted -= OnRewardedAuctionStarted;
        _rewardedAd.OnAuctionSucceed -= OnRewardedAuctionSucceed;
        _rewardedAd.OnAuctionFailed -= OnRewardedAuctionFailed;
        _rewardedAd.OnRoundStarted -= OnRewardedRoundStarted;
        _rewardedAd.OnRoundSucceed -= OnRewardedRoundSucceed;
        _rewardedAd.OnRoundFailed -= OnRewardedRoundFailed;
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

    private void OnInterstitialAuctionStarted(object sender, BidonAuctionStartedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Interstitial] OnAuctionStarted raised");
    }

    private void OnInterstitialAuctionSucceed(object sender, BidonAuctionSucceedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Interstitial] OnAuctionSucceed raised");

        foreach (var result in args.AuctionResults?.ToList())
        {
            Debug.Log($"[BidonPlugin] [Interstitial] [OnAuctionSucceed] result: {result.ToJsonString(true)}");
        }
    }

    private void OnInterstitialAuctionFailed(object sender, BidonAuctionFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnAuctionFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnInterstitialRoundStarted(object sender, BidonRoundStartedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnRoundStarted raised. Id: {args.RoundId}, price floor: {args.PriceFloor}");
    }

    private void OnInterstitialRoundSucceed(object sender, BidonRoundSucceedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Interstitial] OnRoundSucceed raised");
        Debug.Log($"[BidonPlugin] [Interstitial] [OnRoundSucceed] id: {args.RoundId}");

        foreach (var result in args.RoundResults.ToList())
        {
            Debug.Log($"[BidonPlugin] [Interstitial] [OnRoundSucceed] result: {result.ToJsonString(true)}");
        }
    }

    private void OnInterstitialRoundFailed(object sender, BidonRoundFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Interstitial] OnRoundFailed raised. Reason: {args.Cause.ToString()}");
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

    private void OnRewardedAuctionStarted(object sender, BidonAuctionStartedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Rewarded] OnAuctionStarted raised");
    }

    private void OnRewardedAuctionSucceed(object sender, BidonAuctionSucceedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Rewarded] OnAuctionSucceed raised");

        foreach (var result in args.AuctionResults?.ToList())
        {
            Debug.Log($"[BidonPlugin] [Rewarded] [OnAuctionSucceed] result: {result.ToJsonString(true)}");
        }
    }

    private void OnRewardedAuctionFailed(object sender, BidonAuctionFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnAuctionFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnRewardedRoundStarted(object sender, BidonRoundStartedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnRoundStarted raised. Id: {args.RoundId}, price floor: {args.PriceFloor}");
    }

    private void OnRewardedRoundSucceed(object sender, BidonRoundSucceedEventArgs args)
    {
        Debug.Log("[BidonPlugin] [Event] [Rewarded] OnRoundSucceed raised");
        Debug.Log($"[BidonPlugin] [Rewarded] [OnRoundSucceed] id: {args.RoundId}");

        foreach (var result in args.RoundResults.ToList())
        {
            Debug.Log($"[BidonPlugin] [Rewarded] [OnRoundSucceed] result: {result.ToJsonString(true)}");
        }
    }

    private void OnRewardedRoundFailed(object sender, BidonRoundFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Rewarded] OnRoundFailed raised. Reason: {args.Cause.ToString()}");
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
