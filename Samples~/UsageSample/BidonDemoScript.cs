// ReSharper disable CheckNamespace

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Bidon.Mediation;

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
        BidonSdk.Instance.OnInitializationFinished += (_, _) =>
        {
            versionText.text = $"plugin v{BidonSdk.PluginVersion} & sdk v{BidonSdk.Instance.GetSdkVersion()}";

            Debug.Log("[BidonPlugin] [Event] [SDK] OnInitializationFinished raised");

            Debug.Log($"[BidonPlugin] [SDK] Is Initialized: {BidonSdk.Instance.IsInitialized()}");
            Debug.Log($"[BidonPlugin] [SDK] Is Test Mode Enabled: {BidonSdk.Instance.IsTestModeEnabled()}");
            Debug.Log($"[BidonPlugin] [SDK] Current Log Level: {BidonSdk.Instance.GetLogLevel()?.ToString() ?? "null"}");
            Debug.Log($"[BidonPlugin] [SDK] Base URL: {BidonSdk.Instance.GetBaseUrl()}");

            Debug.Log($"[BidonPlugin] [Segment] Uid: {BidonSdk.Instance.Segment.Uid ?? "null"}");
        };

        BidonSdk.Instance.SetLogLevel((BidonLogLevel)logLevelDropdown.value);
        BidonSdk.Instance.SetTestMode(testModeToggle.isOn);

        BidonSdk.Instance.SetBaseUrl("https://b.appbaqend.com");

        CheckSegment();
        CheckRegulations();

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
        BidonSdk.Instance.RegisterAdapter(BidonConstants.AdapterNames.GoogleMobileAds);
#elif UNITY_IOS
        BidonSdk.Instance.RegisterAdapter(BidonConstants.AdapterNames.AppLovin);
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

    private static void CheckSegment()
    {
        Debug.Log($"[BidonPlugin] [Segment] Age (1/2): {BidonSdk.Instance.Segment.Age?.ToString() ?? "null"}");
        BidonSdk.Instance.Segment.Age = 42;
        Debug.Log($"[BidonPlugin] [Segment] Age (2/2): {BidonSdk.Instance.Segment.Age?.ToString() ?? "null"}");

        Debug.Log($"[BidonPlugin] [Segment] Gender (1/2): {BidonSdk.Instance.Segment.Gender?.ToString() ?? "null"}");
        BidonSdk.Instance.Segment.Gender = BidonUserGender.Male;
        Debug.Log($"[BidonPlugin] [Segment] Gender (2/2): {BidonSdk.Instance.Segment.Gender?.ToString() ?? "null"}");

        Debug.Log($"[BidonPlugin] [Segment] Level (1/2): {BidonSdk.Instance.Segment.Level?.ToString() ?? "null"}");
        BidonSdk.Instance.Segment.Level = 11;
        Debug.Log($"[BidonPlugin] [Segment] Level (2/2): {BidonSdk.Instance.Segment.Level?.ToString() ?? "null"}");

        Debug.Log($"[BidonPlugin] [Segment] In-Apps Amount (1/2): {BidonSdk.Instance.Segment.TotalInAppsAmount?.ToString() ?? "null"}");
        BidonSdk.Instance.Segment.TotalInAppsAmount = Double.MaxValue;
        Debug.Log($"[BidonPlugin] [Segment] In-Apps Amount (2/2): {BidonSdk.Instance.Segment.TotalInAppsAmount?.ToString() ?? "null"}");

        Debug.Log($"[BidonPlugin] [Segment] Is Paying (1/2): {BidonSdk.Instance.Segment.IsPaying}");
        BidonSdk.Instance.Segment.IsPaying = true;
        Debug.Log($"[BidonPlugin] [Segment] Is Paying (2/2): {BidonSdk.Instance.Segment.IsPaying}");

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
    }

    private static void CheckRegulations()
    {
        // GDPR
        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Is Applied (1/2): {BidonSdk.Instance.Regulation.IsGdprApplied}");
        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Has Consent (1/2): {BidonSdk.Instance.Regulation.HasGdprConsent}");

        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Applicability Status (1/2): {BidonSdk.Instance.Regulation.GdprApplicabilityStatus}");
        BidonSdk.Instance.Regulation.GdprApplicabilityStatus = BidonGdprApplicabilityStatus.Applies;
        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Applicability Status (2/2): {BidonSdk.Instance.Regulation.GdprApplicabilityStatus}");

        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Consent String (1/2): {BidonSdk.Instance.Regulation.GdprConsentString ?? "null"}");
        BidonSdk.Instance.Regulation.GdprConsentString = "gdpr_consent_string";
        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Consent String (2/2): {BidonSdk.Instance.Regulation.GdprConsentString ?? "null"}");

        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Is Applied (2/2): {BidonSdk.Instance.Regulation.IsGdprApplied}");
        Debug.Log($"[BidonPlugin] [Regulation] [Gdpr] Has Consent (2/2): {BidonSdk.Instance.Regulation.HasGdprConsent}");

        // CCPA
        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Is Applied (1/2): {BidonSdk.Instance.Regulation.IsCcpaApplied}");
        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Has Consent (1/2): {BidonSdk.Instance.Regulation.HasCcpaConsent}");

        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Us Privacy String (1/2): {BidonSdk.Instance.Regulation.UsPrivacyString ?? "null"}");
        BidonSdk.Instance.Regulation.UsPrivacyString = "us_privacy_string";
        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Us Privacy String (2/2): {BidonSdk.Instance.Regulation.UsPrivacyString ?? "null"}");

        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Is Applied (2/2): {BidonSdk.Instance.Regulation.IsCcpaApplied}");
        Debug.Log($"[BidonPlugin] [Regulation] [Ccpa] Has Consent (2/2): {BidonSdk.Instance.Regulation.HasCcpaConsent}");

        // COPPA
        Debug.Log($"[BidonPlugin] [Regulation] [Coppa] Is Applied (1/2): {BidonSdk.Instance.Regulation.IsCoppaApplied}");

        Debug.Log($"[BidonPlugin] [Regulation] [Coppa] Applicability Status (1/2): {BidonSdk.Instance.Regulation.CoppaApplicabilityStatus}");
        BidonSdk.Instance.Regulation.CoppaApplicabilityStatus = BidonCoppaApplicabilityStatus.Yes;
        Debug.Log($"[BidonPlugin] [Regulation] [Coppa] Applicability Status (2/2): {BidonSdk.Instance.Regulation.CoppaApplicabilityStatus}");

        Debug.Log($"[BidonPlugin] [Regulation] [Coppa] Is Applied (2/2): {BidonSdk.Instance.Regulation.IsCoppaApplied}");
    }
}
