using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public interface IBidonSdk
    {
        event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;
        void SetLogLevel(BidonLogLevel logLevel);
        void SetBaseUrl(string baseUrl);
        void RegisterDefaultAdapters();
        void RegisterAdapter(string className);
        void Initialize(string appKey);
        string GetSdkVersion();
        BidonLogLevel GetLogLevel();
        string GetBaseUrl();
        bool IsInitialized();
    }
}
