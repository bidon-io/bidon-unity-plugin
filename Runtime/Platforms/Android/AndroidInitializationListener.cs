#if UNITY_ANDROID
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AndroidInitializationListener : AndroidJavaProxy, IAndroidInInitializationListener
    {
        private readonly IAndroidInInitializationListener _listener;

        internal AndroidInitializationListener(IAndroidInInitializationListener listener) : base("org.bidon.sdk.config.InitializationCallback")
        {
            _listener = listener;
        }

        public void onFinished()
        {
            SyncContextHelper.Post(obj => _listener?.onFinished());
        }
    }
}
#endif
