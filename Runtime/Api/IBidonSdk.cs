// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface IBidonSdk
    {
        event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;
        IBidonSegment Segment { get; }
        IBidonRegulation Regulation { get; }
        void SetLogLevel(BidonLogLevel logLevel);
        void SetTestMode(bool isEnabled);
        bool IsTestModeEnabled();
        void SetBaseUrl(string baseUrl);
        void SetExtraData(string key, object value);
        IDictionary<string, object> GetExtraData();
        void RegisterDefaultAdapters();
        void RegisterAdapter(string className);
        void Initialize(string appKey);
        string GetSdkVersion();
        BidonLogLevel? GetLogLevel();
        string GetBaseUrl();
        bool IsInitialized();
    }
}
