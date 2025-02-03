#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal abstract class AndroidBidonAdBase : IDisposable
    {
        protected AndroidJavaObject ActivityJavaObject;

        private bool _disposed;

        internal AndroidBidonAdBase()
        {
            ActivityJavaObject = AndroidBidonFactory.SafeGetCurrentActivityJavaObject();
        }

        ~AndroidBidonAdBase() => Dispose(false);

        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ActivityJavaObject?.Dispose();
                ActivityJavaObject = null;
            }
        }

        protected bool IsDisposed()
        {
            if (!_disposed) return false;
            Debug.LogError($"[BidonPlugin] Instance of '{GetType().FullName}' has been disposed. Further method calls on this instance are not allowed");
            return true;
        }
    }
}
#endif
