#if UNITY_ANDROID || UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Threading;
using UnityEngine;
using UnityEngine.Scripting;

namespace Bidon.Mediation
{
    public static class SyncContextHelper
    {
        private static SynchronizationContext _unitySynchronizationContext;

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void GetContext() => _unitySynchronizationContext = SynchronizationContext.Current;

        public static void Post(SendOrPostCallback d, object state = null) => _unitySynchronizationContext?.Post(d, state);
    }
}
#endif
