#if UNITY_ANDROID
using System;
using System.Collections.Generic;
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

            _bidonSdkJavaClass.CallStatic<AndroidJavaObject>("setInitializationCallback", new AndroidInitializationListener(this));
        }

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            AndroidJavaClass logLevelJavaClass;
            try
            {
                logLevelJavaClass = new AndroidJavaClass("org.bidon.sdk.logs.logging.Logger$Level");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
                return;
            }

            switch (logLevel)
            {
                case BidonLogLevel.Off:
                {
                    _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setLoggerLevel", logLevelJavaClass.CallStatic<AndroidJavaObject>("valueOf", "Off"));
                    break;
                }
                case BidonLogLevel.Error:
                {
                    _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setLoggerLevel", logLevelJavaClass.CallStatic<AndroidJavaObject>("valueOf", "Error"));
                    break;
                }
                case BidonLogLevel.Verbose:
                case BidonLogLevel.Debug:
                case BidonLogLevel.Info:
                case BidonLogLevel.Warning:
                {
                    _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setLoggerLevel", logLevelJavaClass.CallStatic<AndroidJavaObject>("valueOf", "Verbose"));
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void SetBaseUrl(string baseUrl)
        {
            _bidonSdkJavaClass?.CallStatic<AndroidJavaObject>("setBaseUrl", baseUrl);
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
            switch (nativeLogLevel)
            {
                case "Off":
                    return BidonLogLevel.Off;
                case "Error":
                    return BidonLogLevel.Error;
                case "Verbose":
                    return BidonLogLevel.Verbose;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativeLogLevel), nativeLogLevel, null);
            }
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
