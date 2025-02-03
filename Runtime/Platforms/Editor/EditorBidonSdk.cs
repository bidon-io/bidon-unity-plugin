#if UNITY_EDITOR

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class EditorBidonSdk : IBidonSdk
    {
        public IBidonSegment Segment { get; }
        public IBidonRegulation Regulation { get; }

#pragma warning disable CS0067
        public event EventHandler<BidonInitializationEventArgs> OnInitializationFinished;
#pragma warning restore CS0067

        internal EditorBidonSdk()
        {
            Segment = new EditorBidonSegment();
            Regulation = new EditorBidonRegulation();
        }

        public void SetLogLevel(BidonLogLevel logLevel) { }

        public void SetTestMode(bool isEnabled) { }

        public bool IsTestModeEnabled() => false;

        public void SetBaseUrl(string baseUrl) { }

        public void SetExtraData(string key, object value) { }

        public IDictionary<string, object> GetExtraData() => new Dictionary<string, object>();

        public void RegisterDefaultAdapters() { }

        public void RegisterAdapter(string className) { }

        public void Initialize(string appKey) { }

        public string GetSdkVersion() => null;

        public BidonLogLevel? GetLogLevel() => null;

        public string GetBaseUrl() => null;

        public bool IsInitialized() => false;
    }
}
#endif
