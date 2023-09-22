#if UNITY_EDITOR
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class EditorBidonSdk : IBidonSdk
    {
        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        internal EditorBidonSdk()
        {
            Segment = new EditorBidonSegment();
            Regulation = new EditorBidonRegulation();
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

        public IDictionary<string, object> GetExtraData()
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
