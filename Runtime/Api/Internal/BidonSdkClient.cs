using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class BidonSdkClient : IBidonSdk
    {
        private readonly IBidonSdk _bidonSdkImpl;

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal BidonSdkClient()
        {
#if UNITY_EDITOR
            _bidonSdkImpl = new EditorBidonSdk();
#elif UNITY_ANDROID
            _bidonSdkImpl = new AndroidBidonSdk();
#elif UNITY_IOS
            _bidonSdkImpl = new IosBidonSdk();
#else
            _bidonSdkImpl = new DummyBidonSdk();
#endif
            _bidonSdkImpl.OnInitializationFinished += (sender, args) => OnInitializationFinished?.Invoke(this, args);
        }

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            _bidonSdkImpl.SetLogLevel(logLevel);
        }

        public void SetBaseUrl(string baseUrl)
        {
            _bidonSdkImpl.SetBaseUrl(baseUrl);
        }

        public void SetExtraData(string key, object value)
        {
            if (String.IsNullOrEmpty(key) || (value is string valueString && String.IsNullOrEmpty(valueString))) return;

            if (value is int || value is long || value is float || value is double || value is bool || value is char || value is string)
            {
                _bidonSdkImpl.SetExtraData(key, value);
            }
        }

        public void RegisterDefaultAdapters()
        {
            _bidonSdkImpl.RegisterDefaultAdapters();
        }

        public void RegisterAdapter(string className)
        {
            _bidonSdkImpl.RegisterAdapter(className);
        }

        public void Initialize(string appKey)
        {
            _bidonSdkImpl.Initialize(appKey);
        }

        public string GetSdkVersion()
        {
            return _bidonSdkImpl.GetSdkVersion();
        }

        public BidonLogLevel GetLogLevel()
        {
            return _bidonSdkImpl.GetLogLevel();
        }

        public string GetBaseUrl()
        {
            return _bidonSdkImpl.GetBaseUrl();
        }

        public bool IsInitialized()
        {
            return _bidonSdkImpl.IsInitialized();
        }
    }
}
