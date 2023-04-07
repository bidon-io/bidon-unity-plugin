#if UNITY_IOS || BIDON_DEV_IOS
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class IosBidonSdk : IBidonSdk
    {
        private static IosBidonSdk _instance;

        private delegate void InitializationFinishedCallback();

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal IosBidonSdk()
        {
            _instance = this;
            SetMetadata(Application.unityVersion, BidonSdk.PluginVersion);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetLogLevel")]
        private static extern void BidonSetLogLevel(int logLevel);

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            BidonSetLogLevel((int)logLevel);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetBaseUrl")]
        private static extern void BidonSetBaseUrl(string baseUrl);

        public void SetBaseUrl(string baseUrl)
        {
            BidonSetBaseUrl(baseUrl);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataInt")]
        private static extern void BidonSetExtraDataInt(string key, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataLong")]
        private static extern void BidonSetExtraDataLong(string key, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataFloat")]
        private static extern void BidonSetExtraDataFloat(string key, float value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataDouble")]
        private static extern void BidonSetExtraDataDouble(string key, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataBool")]
        private static extern void BidonSetExtraDataBool(string key, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataChar")]
        private static extern void BidonSetExtraDataChar(string key, char value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetExtraDataString")]
        private static extern void BidonSetExtraDataString(string key, string value);

        public void SetExtraData(string key, object value)
        {
            switch (value)
            {
                case int valueInt:
                    BidonSetExtraDataInt(key, valueInt);
                    break;
                case long valueLong:
                    BidonSetExtraDataLong(key, valueLong);
                    break;
                case float valueFloat:
                    BidonSetExtraDataFloat(key, valueFloat);
                    break;
                case double valueDouble:
                    BidonSetExtraDataDouble(key, valueDouble);
                    break;
                case bool valueBool:
                    BidonSetExtraDataBool(key, valueBool);
                    break;
                case char valueChar:
                    BidonSetExtraDataChar(key, valueChar);
                    break;
                case string valueString:
                    BidonSetExtraDataString(key, valueString);
                    break;
            }
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegisterDefaultAdapters")]
        private static extern void BidonRegisterDefaultAdapters();

        public void RegisterDefaultAdapters()
        {
            BidonRegisterDefaultAdapters();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginRegisterAdapter")]
        private static extern void BidonRegisterAdapter(string className);

        public void RegisterAdapter(string className)
        {
            BidonRegisterAdapter(className);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginInitialize")]
        private static extern void BidonInitialize(string appKey, InitializationFinishedCallback callback);

        public void Initialize(string appKey)
        {
            BidonInitialize(appKey, InitializationFinished);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginGetSdkVersion")]
        private static extern string BidonGetSdkVersion();

        public string GetSdkVersion()
        {
            return BidonGetSdkVersion();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginGetLogLevel")]
        private static extern BidonLogLevel BidonGetLogLevel();

        public BidonLogLevel GetLogLevel()
        {
            return BidonGetLogLevel();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginGetBaseUrl")]
        private static extern string BidonGetBaseUrl();

        public string GetBaseUrl()
        {
            return BidonGetBaseUrl();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginIsInitialized")]
        private static extern bool BidonIsInitialized();

        public bool IsInitialized()
        {
            return BidonIsInitialized();
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSetMetadata")]
        private static extern void BidonSetMetadata(string frameworkVersion, string pluginVersion);

        private void SetMetadata(string frameworkVersion, string pluginVersion)
        {
            BidonSetMetadata(frameworkVersion, pluginVersion);
        }

        [MonoPInvokeCallback(typeof(InitializationFinishedCallback))]
        private static void InitializationFinished()
        {
            SyncContextHelper.Post(state => _instance.OnInitializationFinished?.Invoke(_instance, new BidonInitializationEventArgs()));
        }
    }
}
#endif
