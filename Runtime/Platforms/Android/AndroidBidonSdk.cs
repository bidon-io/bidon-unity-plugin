#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Bidon.Mediation
{
    internal class AndroidBidonSdk : IBidonSdk, IAndroidInInitializationListener
    {
        private readonly AndroidJavaClass _bidonSdkJavaClass;
        private readonly AndroidJavaObject _activityJavaObject;

        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal AndroidBidonSdk()
        {
            _bidonSdkJavaClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.BidonSdk");
            _activityJavaObject = AndroidBidonFactory.SafeGetCurrentActivityJavaObject();

            if (_bidonSdkJavaClass == null || _activityJavaObject == null) return;

            Segment = new AndroidBidonSegment(_bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("getSegment"));
            Regulation = new AndroidBidonRegulation(_bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("getRegulation"));

            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setFramework", "unity");
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setFrameworkVersion", Application.unityVersion);
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setPluginVersion", BidonSdk.PluginVersion);

            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setInitializationCallback", new AndroidInitializationListener(this));
        }

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setLoggerLevel", logLevel.ToJavaObject());
        }

        public void SetTestMode(bool isEnabled)
        {
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setTestMode", isEnabled);
        }

        public bool IsTestModeEnabled()
        {
            return _bidonSdkJavaClass.SafeCallStatic<bool>("isTestMode");
        }

        public void SetBaseUrl(string baseUrl)
        {
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("setBaseUrl", baseUrl);
        }

        public void SetExtraData(string key, object value)
        {
            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;

            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("addExtra", key, AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public IDictionary<string, object> GetExtraData()
        {
            return AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("getExtras"));
        }

        public void RegisterDefaultAdapters()
        {
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("registerDefaultAdapters");
        }

        public void RegisterAdapter(string className)
        {
            _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("registerAdapter", className);
        }

        public void Initialize(string appKey)
        {
            _bidonSdkJavaClass.SafeCallStatic("initialize", _activityJavaObject, appKey);
        }

        public string GetSdkVersion()
        {
            return _bidonSdkJavaClass.SafeGetStatic<string>("SdkVersion");
        }

        public BidonLogLevel? GetLogLevel()
        {
            return _bidonSdkJavaClass.SafeCallStatic<AndroidJavaObject>("getLoggerLevel").ToBidonLogLevel();
        }

        public string GetBaseUrl()
        {
            return _bidonSdkJavaClass.SafeCallStatic<string>("getBaseUrl");
        }

        public bool IsInitialized()
        {
            return _bidonSdkJavaClass.SafeCallStatic<bool>("isInitialized");
        }

        [Preserve]
        public void onFinished()
        {
            OnInitializationFinished?.Invoke(this, new BidonInitializationEventArgs());
        }
    }
}
#endif
