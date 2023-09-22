using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Bidon.Mediation;

// ReSharper disable once CheckNamespace
public class BidonBannerAdManager : MonoBehaviour
{
    private BidonBannerAd _bannerAd;

    [SerializeField] private InputField priceFloorInputField;
    [SerializeField] private Dropdown bannerFormatDropdown;
    [SerializeField] private Dropdown bannerPositionDropdown;

    private void Awake()
    {
        Assert.IsNotNull(priceFloorInputField);
        Assert.IsNotNull(bannerFormatDropdown);
        Assert.IsNotNull(bannerPositionDropdown);
        bannerFormatDropdown.ClearOptions();
        bannerFormatDropdown.AddOptions(Enum.GetNames(typeof(BidonBannerFormat)).ToList());
        bannerPositionDropdown.ClearOptions();
        bannerPositionDropdown.AddOptions(Enum.GetNames(typeof(BidonBannerPosition)).ToList());
        bannerPositionDropdown.onValueChanged.AddListener(delegate
        {
            if (_bannerAd == null)
            {
                Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
                return;
            }
            _bannerAd.SetPredefinedPosition((BidonBannerPosition)bannerPositionDropdown.value);
        });
    }

    public void CreateAd()
    {
        if (!BidonSdk.Instance.IsInitialized())
        {
            Debug.LogWarning($"[BidonPlugin] [Banner] Initialize Bidon SDK first");
            return;
        }

        if (_bannerAd != null)
        {
            Debug.LogWarning($"[BidonPlugin] [Banner] Destroy previous instance first");
            return;
        }

        _bannerAd = new BidonBannerAd();
        SubscribeToBannerEvents();

        _bannerAd.SetExtraData("banner_bool_key", false);
        _bannerAd.SetExtraData("banner_char_key", 'b');
        _bannerAd.SetExtraData("banner_int_key", Int32.MinValue);
        _bannerAd.SetExtraData("banner_long_key", Int64.MaxValue);
        _bannerAd.SetExtraData("banner_float_key", Single.MinValue);
        _bannerAd.SetExtraData("banner_double_key", Double.MaxValue);
        _bannerAd.SetExtraData("banner_string_key", "banner_string_value");
        _bannerAd.SetExtraData("banner_unwanted_key", false);
        _bannerAd.SetExtraData("banner_unwanted_key", null);

        string extraData = String.Join(", ", _bannerAd.GetExtraData()
            .Select(kvp => $"{kvp.Key}:({kvp.Value.GetType()}){kvp.Value}")
            .ToArray());
        Debug.Log($"[BidonPlugin] [Banner] Extra Data: {extraData}");

        _bannerAd.SetFormat((BidonBannerFormat)bannerFormatDropdown.value);
        Debug.Log($"[BidonPlugin] [Banner] Format: {_bannerAd.GetFormat()}");
    }

    public void LoadAd()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        bool isParsed = Double.TryParse(priceFloorInputField.text, out double priceFloor);
        _bannerAd.Load(isParsed ? priceFloor : 0.01d);
    }

    public void IsReady()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        Debug.Log($"[BidonPlugin] [Banner] Is Ready: {_bannerAd.IsReady()}");
    }

    public void ShowAdAtPredefinedPos()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        _bannerAd.SetPredefinedPosition((BidonBannerPosition)bannerPositionDropdown.value);
        _bannerAd.Show();
    }

    public void ShowAdAtCustomPos()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        int xOffset = Screen.width / 2;
        int yOffset = Screen.height / 6;
        _bannerAd.SetCustomPositionAndRotation(new Vector2Int(xOffset, yOffset), -8, new Vector2(0.5f, 0f));
        _bannerAd.Show();
    }

    public void HideAd()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        Debug.Log($"[BidonPlugin] [Banner] Is Showing: {_bannerAd.IsShowing()}");
        _bannerAd.Hide();
    }

    public void DestroyAd()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        UnsubscribeFromBannerEvents();
        _bannerAd.Dispose();
        _bannerAd = null;
    }

    public void NotifyWin()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        _bannerAd.NotifyWin();
    }

    public void NotifyLoss()
    {
        if (_bannerAd == null)
        {
            Debug.LogWarning("[BidonPlugin] [Banner] Create new instance first");
            return;
        }
        _bannerAd.NotifyLoss("some_winner_id", 0.2d);
    }

    private void SubscribeToBannerEvents()
    {
        _bannerAd.OnAdLoaded += OnBannerAdLoaded;
        _bannerAd.OnAdLoadFailed += OnBannerAdLoadFailed;
        _bannerAd.OnAdShown += OnBannerAdShown;
        _bannerAd.OnAdShowFailed += OnBannerAdShowFailed;
        _bannerAd.OnAdClicked += OnBannerAdClicked;
        _bannerAd.OnAdExpired += OnBannerAdExpired;
        _bannerAd.OnAdRevenueReceived += OnBannerAdRevenueReceived;
    }

    private void UnsubscribeFromBannerEvents()
    {
        _bannerAd.OnAdLoaded -= OnBannerAdLoaded;
        _bannerAd.OnAdLoadFailed -= OnBannerAdLoadFailed;
        _bannerAd.OnAdShown -= OnBannerAdShown;
        _bannerAd.OnAdShowFailed -= OnBannerAdShowFailed;
        _bannerAd.OnAdClicked -= OnBannerAdClicked;
        _bannerAd.OnAdExpired -= OnBannerAdExpired;
        _bannerAd.OnAdRevenueReceived -= OnBannerAdRevenueReceived;
    }

    #region Banner Ad Callbacks
    private void OnBannerAdLoaded(object sender, BidonAdLoadedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdLoaded raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnBannerAdLoadFailed(object sender, BidonAdLoadFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdLoadFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnBannerAdShown(object sender, BidonAdShownEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdShown raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnBannerAdShowFailed(object sender, BidonAdShowFailedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdShowFailed raised. Reason: {args.Cause.ToString()}");
    }

    private void OnBannerAdClicked(object sender, BidonAdClickedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdClicked raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnBannerAdExpired(object sender, BidonAdExpiredEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdExpired raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}");
    }

    private void OnBannerAdRevenueReceived(object sender, BidonAdRevenueReceivedEventArgs args)
    {
        Debug.Log($"[BidonPlugin] [Event] [Banner] OnAdRevenueReceived raised. Ad: {args.Ad?.ToJsonString(true) ?? "null"}, AdValue: {args.AdValue?.ToJsonString(true) ?? "null"}");
    }
    #endregion
}
