#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;
using UnityEngine.Scripting;

namespace Bidon.Mediation
{
    public class AndroidInitializationListener : AndroidJavaProxy, IAndroidInInitializationListener
    {
        private readonly IAndroidInInitializationListener _listener;

        internal AndroidInitializationListener(IAndroidInInitializationListener listener) : base("org.bidon.sdk.config.InitializationCallback")
        {
            _listener = listener;
        }

        [Preserve]
        public void onFinished()
        {
            SyncContextHelper.Post(obj => _listener?.onFinished());
        }
    }
}
#endif
