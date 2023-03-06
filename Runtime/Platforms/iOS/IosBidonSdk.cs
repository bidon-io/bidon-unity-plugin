#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
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

        [MonoPInvokeCallback(typeof(InitializationFinishedCallback))]
        private static void InitializationFinished()
        {
            SyncContextHelper.Post(state => _instance.OnInitializationFinished?.Invoke(_instance, new BidonInitializationEventArgs()));
        }
    }
}
#endif
