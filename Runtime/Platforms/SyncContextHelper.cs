#if UNITY_ANDROID || UNITY_IOS
using System.Threading;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    public static class SyncContextHelper
    {
        private static SynchronizationContext _unitySynchronizationContext;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void GetContext() => _unitySynchronizationContext = SynchronizationContext.Current;

        public static void Post(SendOrPostCallback d, object state = null)
        {
            _unitySynchronizationContext.Post(d, state);
        }
    }
}
#endif
