using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class BidonSdkClient : IBidonSdk
    {
        private readonly IBidonSdk _bidonSdkImpl;

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

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

            Segment = _bidonSdkImpl.Segment;
            Regulation = _bidonSdkImpl.Regulation;
        }

        public void SetLogLevel(BidonLogLevel logLevel) => _bidonSdkImpl.SetLogLevel(logLevel);

        public void SetTestMode(bool isEnabled) => _bidonSdkImpl.SetTestMode(isEnabled);

        public bool IsTestModeEnabled() => _bidonSdkImpl.IsTestModeEnabled();

        public void SetBaseUrl(string baseUrl) => _bidonSdkImpl.SetBaseUrl(baseUrl);

        public void SetExtraData(string key, object value)
        {
            if (String.IsNullOrEmpty(key)) return;
            if (!(value is bool) && !(value is char) && !(value is int) && !(value is long) && !(value is float)
                && !(value is double) && !(value is string) && value != null) return;
            _bidonSdkImpl.SetExtraData(key, value);
        }

        public IDictionary<string, object> GetExtraData() => _bidonSdkImpl.GetExtraData();

        public void RegisterDefaultAdapters() => _bidonSdkImpl.RegisterDefaultAdapters();

        public void RegisterAdapter(string className) => _bidonSdkImpl.RegisterAdapter(className);

        public void Initialize(string appKey) => _bidonSdkImpl.Initialize(appKey);

        public string GetSdkVersion() => _bidonSdkImpl.GetSdkVersion();

        public BidonLogLevel GetLogLevel() => _bidonSdkImpl.GetLogLevel();

        public string GetBaseUrl() => _bidonSdkImpl.GetBaseUrl();

        public bool IsInitialized() => _bidonSdkImpl.IsInitialized();
    }
}
