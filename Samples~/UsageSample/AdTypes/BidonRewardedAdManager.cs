using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Bidon.Mediation;

// ReSharper disable once CheckNamespace
public class BidonRewardedAdManager : MonoBehaviour
{
    private BidonRewardedAd _rewardedAd;

    [SerializeField] private InputField priceFloorInputField;

    private void Awake()
    {
        Assert.IsNotNull(priceFloorInputField);
    }

    public void CreateAd()
    {
        if (!BidonSdk.Instance.IsInitialized())
        {
            Debug.LogWarning($"[BidonPlugin] [Rewarded] Initialize Bidon SDK first");
            return;
        }

        if (_rewardedAd != null)
        {
            Debug.LogWarning($"[BidonPlugin] [Rewarded] Destroy previous instance first");
            return;
        }

        _rewardedAd = new BidonRewardedAd();
        SubscribeToRewardedEvents();

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
        Debug.Log($"[BidonPlugin] [Rewarded] Extra Data: {extraData}");
    }

    public void LoadAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        bool isParsed = Double.TryParse(priceFloorInputField.text, out double priceFloor);
        _rewardedAd.Load(isParsed ? priceFloor : 0.03d);
    }

    public void IsReady()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        Debug.Log($"[BidonPlugin] [Rewarded] Is Ready: {_rewardedAd.IsReady()}");
    }

    public void ShowAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        _rewardedAd.Show();
    }

    public void DestroyAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        UnsubscribeFromRewardedEvents();
        _rewardedAd.Dispose();
        _rewardedAd = null;
    }

    public void NotifyWin()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        _rewardedAd.NotifyWin();
    }

    public void NotifyLoss()
    {
        if (_rewardedAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Rewarded] Create new instance first");
            return;
        }
        _rewardedAd.NotifyLoss("some_winner_id", 0.9d);
    }

    private void SubscribeToRewardedEvents()
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

    #region Rewarded Ad Callbacks
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
