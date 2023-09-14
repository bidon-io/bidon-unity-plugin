#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class AndroidBidonSdk : IBidonSdk, IAndroidInInitializationListener
    {
        private readonly AndroidJavaObject _bidonSdkJavaClass;
        private readonly AndroidJavaObject _activityJavaObject;

        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal AndroidBidonSdk()
        {
            try
            {
                _bidonSdkJavaClass = new AndroidJavaClass("org.bidon.sdk.BidonSdk");
                _activityJavaObject = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
                return;
            }

            Segment = new AndroidBidonSegment(_bidonSdkJavaClass.CallStatic<AndroidJavaObject>("getSegment"));
            Regulation = new AndroidBidonRegulation(_bidonSdkJavaClass.CallStatic<AndroidJavaObject>("getRegulation"));

            _bidonSdkJavaClass.CallStatic<AndroidJavaObject>("setFramework", "unity");
            _bidonSdkJavaClass.CallStatic<AndroidJavaObject>("setFrameworkVersion", Application.unityVersion);
            _bidonSdkJavaClass.CallStatic<AndroidJavaObject>("setPluginVersion", BidonSdk.PluginVersion);

            _bidonSdkJavaClass.CallStatic<AndroidJavaObject>("setInitializationCallback", new AndroidInitializationListener(this));
        }

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setLoggerLevel", AndroidBidonJavaHelper.GetLogLevelJavaObject(logLevel));
        }

        public void SetTestMode(bool isEnabled)
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setTestMode", isEnabled);
        }

        public bool IsTestModeEnabled()
        {
            Debug.Log("Method IsTestModeEnabled() is not yet supported on Android Platform");
            return false;
        }

        public void SetBaseUrl(string baseUrl)
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setBaseUrl", baseUrl);
        }

        public void SetExtraData(string key, object value)
        {
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("addExtra", key,
                value == null ? null : AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public void RegisterDefaultAdapters()
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("registerDefaultAdapters");
        }

        public void RegisterAdapter(string className)
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("registerAdapter", className);
        }

        public void Initialize(string appKey)
        {
            _bidonSdkJavaClass?.CallStatic("initialize", _activityJavaObject, appKey);
        }

        public string GetSdkVersion()
        {
            return _bidonSdkJavaClass?.GetStatic<string>("SdkVersion") ?? String.Empty;
        }

        public BidonLogLevel GetLogLevel()
        {
            string nativeLogLevel = _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("getLoggerLevel").Call<string>("name");

            return nativeLogLevel switch
            {
                "Off" => BidonLogLevel.Off,
                "Error" => BidonLogLevel.Error,
                "Verbose" => BidonLogLevel.Verbose,
                _ => throw new ArgumentOutOfRangeException(nameof(nativeLogLevel), nativeLogLevel, null)
            };
        }

        public string GetBaseUrl()
        {
            Debug.Log("Method GetBaseUrl() is not yet supported on Android Platform");
            return String.Empty;
        }

        public bool IsInitialized()
        {
            return _bidonSdkJavaClass?.CallStatic<bool>("isInitialized") ?? false;
        }

        #region Callbacks

        public void onFinished()
        {
            OnInitializationFinished?.Invoke(this, new BidonInitializationEventArgs());
        }

        #endregion
    }
}
#endif
