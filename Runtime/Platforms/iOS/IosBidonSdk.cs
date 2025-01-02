#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class IosBidonSdk : IBidonSdk
    {
        private static IosBidonSdk _instance;

        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

        private delegate void InitializationFinishedCallback();

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal IosBidonSdk()
        {
            _instance = this;

            Segment = new IosBidonSegment();
            Regulation = new IosBidonRegulation();

            SetMetadata(Application.unityVersion, BidonSdk.PluginVersion);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetLogLevel")]
        private static extern void BidonSdkSetLogLevel(int logLevel);

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            BidonSdkSetLogLevel((int)logLevel);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetTestMode")]
        private static extern void BidonSdkSetTestMode(bool isEnabled);

        public void SetTestMode(bool isEnabled)
        {
            BidonSdkSetTestMode(isEnabled);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkIsTestModeEnabled")]
        private static extern bool BidonSdkIsTestModeEnabled();

        public bool IsTestModeEnabled()
        {
            return BidonSdkIsTestModeEnabled();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetBaseUrl")]
        private static extern void BidonSdkSetBaseUrl(string baseUrl);

        public void SetBaseUrl(string baseUrl)
        {
            BidonSdkSetBaseUrl(baseUrl);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataBool")]
        private static extern void BidonSdkSetExtraDataBool(string key, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataInt")]
        private static extern void BidonSdkSetExtraDataInt(string key, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataLong")]
        private static extern void BidonSdkSetExtraDataLong(string key, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataFloat")]
        private static extern void BidonSdkSetExtraDataFloat(string key, float value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataDouble")]
        private static extern void BidonSdkSetExtraDataDouble(string key, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataString")]
        private static extern void BidonSdkSetExtraDataString(string key, string value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetExtraDataNull")]
        private static extern void BidonSdkSetExtraDataNull(string key);

        public void SetExtraData(string key, object value)
        {
            switch (value)
            {
                case bool valueBool:
                    BidonSdkSetExtraDataBool(key, valueBool);
                    break;
                case char valueChar:
                    BidonSdkSetExtraDataString(key, valueChar.ToString());
                    break;
                case int valueInt:
                    BidonSdkSetExtraDataInt(key, valueInt);
                    break;
                case long valueLong:
                    BidonSdkSetExtraDataLong(key, valueLong);
                    break;
                case float valueFloat:
                    BidonSdkSetExtraDataFloat(key, valueFloat);
                    break;
                case double valueDouble:
                    BidonSdkSetExtraDataDouble(key, valueDouble);
                    break;
                case string valueString:
                    BidonSdkSetExtraDataString(key, valueString);
                    break;
                case null:
                    BidonSdkSetExtraDataNull(key);
                    break;
            }
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkGetExtraData")]
        private static extern string BidonSdkGetExtraData();

        public IDictionary<string, object> GetExtraData()
        {
            return IosBidonHelper.GetDictionaryFromJsonString(BidonSdkGetExtraData());
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkRegisterDefaultAdapters")]
        private static extern void BidonSdkRegisterDefaultAdapters();

        public void RegisterDefaultAdapters()
        {
            BidonSdkRegisterDefaultAdapters();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkRegisterAdapter")]
        private static extern void BidonSdkRegisterAdapter(string className);

        public void RegisterAdapter(string className)
        {
            BidonSdkRegisterAdapter(className);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkInitialize")]
        private static extern void BidonSdkInitialize(string appKey, InitializationFinishedCallback callback);

        public void Initialize(string appKey)
        {
            BidonSdkInitialize(appKey, InitializationFinished);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkGetVersion")]
        private static extern string BidonSdkGetVersion();

        public string GetSdkVersion()
        {
            return BidonSdkGetVersion();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkGetLogLevel")]
        private static extern int BidonSdkGetLogLevel();

        public BidonLogLevel? GetLogLevel()
        {
            return BidonSdkGetLogLevel().ToNullableEnum<BidonLogLevel>();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkGetBaseUrl")]
        private static extern string BidonSdkGetBaseUrl();

        public string GetBaseUrl()
        {
            return BidonSdkGetBaseUrl();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkIsInitialized")]
        private static extern bool BidonSdkIsInitialized();

        public bool IsInitialized()
        {
            return BidonSdkIsInitialized();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSdkSetMetadata")]
        private static extern void BidonSdkSetMetadata(string frameworkVersion, string pluginVersion);

        private void SetMetadata(string frameworkVersion, string pluginVersion)
        {
            BidonSdkSetMetadata(frameworkVersion, pluginVersion);
        }

        [MonoPInvokeCallback(typeof(InitializationFinishedCallback))]
        private static void InitializationFinished()
        {
            SyncContextHelper.Post(state => _instance.OnInitializationFinished?.Invoke(_instance, new BidonInitializationEventArgs()));
        }
    }
}
#endif
