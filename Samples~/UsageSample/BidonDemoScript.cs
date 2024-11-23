using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Bidon.Mediation;

// ReSharper disable once CheckNamespace
public class BidonDemoScript : MonoBehaviour
{
    [SerializeField] private Button initButton;
    [SerializeField] private Text versionText;
    [SerializeField] private Dropdown logLevelDropdown;
    [SerializeField] private Toggle testModeToggle;

    private void Awake()
    {
        Assert.IsNotNull(initButton);
        Assert.IsNotNull(versionText);
        Assert.IsNotNull(logLevelDropdown);
        Assert.IsNotNull(testModeToggle);
        logLevelDropdown.ClearOptions();
        logLevelDropdown.AddOptions(Enum.GetNames(typeof(BidonLogLevel)).ToList());
        versionText.text = $"plugin v{BidonSdk.PluginVersion}";
    }

    public void InitBidonSdk()
    {
        BidonSdk.Instance.OnInitializationFinished += (sender, args) =>
        {
            versionText.text = $"plugin v{BidonSdk.PluginVersion} & sdk v{BidonSdk.Instance.GetSdkVersion()}";

            Debug.Log("[BidonPlugin] [Event] [SDK] OnInitializationFinished raised");
            Debug.Log($"[BidonPlugin] [SDK] Is Initialized: {BidonSdk.Instance.IsInitialized()}");
            Debug.Log($"[BidonPlugin] [SDK] Current Log Level: {BidonSdk.Instance.GetLogLevel().ToString()}");
            Debug.Log($"[BidonPlugin] [SDK] Is Test Mode Enabled: {BidonSdk.Instance.IsTestModeEnabled()}");
            Debug.Log($"[BidonPlugin] [Segment] Uid: {BidonSdk.Instance.Segment.Uid}");
            Debug.Log($"[BidonPlugin] [SDK] Base URL: {BidonSdk.Instance.GetBaseUrl()}");
        };

        BidonSdk.Instance.SetLogLevel((BidonLogLevel)logLevelDropdown.value);
        BidonSdk.Instance.SetTestMode(testModeToggle.isOn);

        BidonSdk.Instance.SetBaseUrl("https://b.appbaqend.com");

        BidonSdk.Instance.Regulation.GdprConsentString = "gdpr_consent_string";
        BidonSdk.Instance.Regulation.UsPrivacyString = "us_privacy_string";
        BidonSdk.Instance.Regulation.GdprConsentStatus = BidonGdprConsentStatus.Given;
        BidonSdk.Instance.Regulation.CoppaApplicabilityStatus = BidonCoppaApplicabilityStatus.Yes;
        Debug.Log($"[BidonPlugin] [Regulation] Gdpr Consent String: {BidonSdk.Instance.Regulation.GdprConsentString}");
        Debug.Log($"[BidonPlugin] [Regulation] Us Privacy String: {BidonSdk.Instance.Regulation.UsPrivacyString}");
        Debug.Log($"[BidonPlugin] [Regulation] Gdpr Consent Status: {BidonSdk.Instance.Regulation.GdprConsentStatus}");
        Debug.Log($"[BidonPlugin] [Regulation] Coppa Applicability Status: {BidonSdk.Instance.Regulation.CoppaApplicabilityStatus}");

        BidonSdk.Instance.Segment.Age = 42;
        BidonSdk.Instance.Segment.Gender = BidonUserGender.Male;
        BidonSdk.Instance.Segment.Level = 11;
        BidonSdk.Instance.Segment.TotalInAppsAmount = Double.MaxValue;
        BidonSdk.Instance.Segment.IsPaying = true;
        Debug.Log($"[BidonPlugin] [Segment] Age: {BidonSdk.Instance.Segment.Age}");
        Debug.Log($"[BidonPlugin] [Segment] Gender: {BidonSdk.Instance.Segment.Gender}");
        Debug.Log($"[BidonPlugin] [Segment] Level: {BidonSdk.Instance.Segment.Level}");
        Debug.Log($"[BidonPlugin] [Segment] In-Apps Amount: {BidonSdk.Instance.Segment.TotalInAppsAmount}");
        Debug.Log($"[BidonPlugin] [Segment] Is Paying: {BidonSdk.Instance.Segment.IsPaying}");

        BidonSdk.Instance.Segment.SetCustomAttribute("segment_bool_attr", true);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_int_attr", Int32.MinValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_long_attr", Int64.MaxValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_double_attr", Double.MinValue);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_string_attr", "segment_string_value");
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unsupported_attr", 'u');
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unwanted_attr", false);
        BidonSdk.Instance.Segment.SetCustomAttribute("segment_unwanted_attr", null);
        string attributes = String.Join(", ",
            BidonSdk.Instance.Segment.CustomAttributes
                .Select(attr => $"{attr.Key}:({attr.Value.GetType()}){attr.Value}")
                .ToArray());
        Debug.Log($"[BidonPlugin] [Segment] Custom Attributes: {attributes}");

        BidonSdk.Instance.SetExtraData("sdk_extra_bool_key", false);
        BidonSdk.Instance.SetExtraData("sdk_extra_char_key", 'v');
        BidonSdk.Instance.SetExtraData("sdk_extra_int_key", 42);
        BidonSdk.Instance.SetExtraData("sdk_extra_long_key", Int64.MaxValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_float_key", Single.MinValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_double_key", Double.MaxValue);
        BidonSdk.Instance.SetExtraData("sdk_extra_string_key", "sdk_extra_string_value");
        BidonSdk.Instance.SetExtraData("sdk_extra_unwanted_key", 11);
        BidonSdk.Instance.SetExtraData("sdk_extra_unwanted_key", null);
        string extraData = String.Join(", ", BidonSdk.Instance.GetExtraData()
            .Select(kvp => $"{kvp.Key}:({kvp.Value.GetType()}){kvp.Value}")
            .ToArray());
        Debug.Log($"[BidonPlugin] [Sdk] Extra Data: {extraData}");

#if UNITY_ANDROID
        BidonSdk.Instance.RegisterAdapter("org.bidon.admob.AdmobAdapter");
#elif UNITY_IOS
        BidonSdk.Instance.RegisterAdapter("BidonAdapterAppLovin.AppLovinDemandSourceAdapter");
#endif

        BidonSdk.Instance.RegisterDefaultAdapters();

#if UNITY_EDITOR
        BidonSdk.Instance.Initialize(String.Empty);
#elif UNITY_ANDROID
        BidonSdk.Instance.Initialize("b1689e101a2555084e08c2ba7375783bde166625bbeae00f");
#elif UNITY_IOS
        BidonSdk.Instance.Initialize("dee74c5129f53fc629a44a690a02296694e3eef99f2d3a5f");
#else
        BidonSdk.Instance.Initialize(String.Empty);
#endif

        initButton.interactable = false;
    }
}
