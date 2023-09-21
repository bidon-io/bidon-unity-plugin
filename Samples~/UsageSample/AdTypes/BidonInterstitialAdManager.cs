using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Bidon.Mediation;

// ReSharper disable once CheckNamespace
public class BidonInterstitialAdManager : MonoBehaviour
{
    private BidonInterstitialAd _interstitialAd;

    [SerializeField] private InputField priceFloorInputField;

    private void Awake()
    {
        Assert.IsNotNull(priceFloorInputField);
    }

    public void CreateAd()
    {
        if (!BidonSdk.Instance.IsInitialized())
        {
            Debug.LogWarning($"[BidonPlugin] [Interstitial] Initialize Bidon SDK first");
            return;
        }

        if (_interstitialAd != null)
        {
            Debug.LogWarning($"[BidonPlugin] [Interstitial] Destroy previous instance first");
            return;
        }

        _interstitialAd = new BidonInterstitialAd();
        SubscribeToInterstitialEvents();

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
        Debug.Log($"[BidonPlugin] [Interstitial] Extra Data: {extraData}");
    }

    public void LoadAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        bool isParsed = Double.TryParse(priceFloorInputField.text, out double priceFloor);
        _interstitialAd.Load(isParsed ? priceFloor : 0.02d);
    }

    public void IsReady()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        Debug.Log($"[BidonPlugin] [Interstitial] Is Ready: {_interstitialAd.IsReady()}");
    }

    public void ShowAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        _interstitialAd.Show();
    }

    public void DestroyAd()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        UnsubscribeFromInterstitialEvents();
        _interstitialAd.Dispose();
        _interstitialAd = null;
    }

    public void NotifyWin()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        _interstitialAd.NotifyWin();
    }

    public void NotifyLoss()
    {
        if (_interstitialAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Interstitial] Create new instance first");
            return;
        }
        _interstitialAd.NotifyLoss("some_winner_id", 0.5d);
    }

    private void SubscribeToInterstitialEvents()
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

    #region Interstitial Ad Callbacks
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
    #endregion
}
