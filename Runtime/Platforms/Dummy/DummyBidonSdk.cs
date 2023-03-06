using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class DummyBidonSdk : IBidonSdk
    {
        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;

        public void SetLogLevel(BidonLogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void SetBaseUrl(string baseUrl)
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
