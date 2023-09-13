#if (!UNITY_ANDROID && !UNITY_EDITOR && !UNITY_IOS) || BIDON_DEV_DUMMY
using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class DummyBidonSdk : IBidonSdk
    {
        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal DummyBidonSdk()
        {
            Segment = new DummyBidonSegment();
            Regulation = new DummyBidonRegulation();
        }

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void SetTestMode(bool isEnabled)
        {
            throw new NotImplementedException();
        }

        public bool IsTestModeEnabled()
        {
            throw new NotImplementedException();
        }

        public void SetBaseUrl(string baseUrl)
        {
            throw new NotImplementedException();
        }

        public void SetExtraData(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void RegisterDefaultAdapters()
        {
            throw new NotImplementedException();
        }

        public void RegisterAdapter(string className)
        {
            throw new NotImplementedException();
        }

        public void Initialize(string appKey)
        {
            throw new NotImplementedException();
        }

        public string GetSdkVersion()
        {
            throw new NotImplementedException();
        }

        public BidonLogLevel GetLogLevel()
        {
            throw new NotImplementedException();
        }

        public string GetBaseUrl()
        {
            throw new NotImplementedException();
        }

        public bool IsInitialized()
        {
            throw new NotImplementedException();
        }
    }
}
#endif
