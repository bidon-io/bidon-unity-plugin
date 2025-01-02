#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal abstract class IosBidonAdBase : IDisposable
    {
        private bool _disposed;

        ~IosBidonAdBase() => Dispose(false);

        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        protected abstract void Dispose(bool disposing);

        protected bool IsDisposed()
        {
            if (!_disposed) return false;
            Debug.LogError($"[BidonPlugin] Instance of '{GetType().FullName}' has been disposed. Further method calls on this instance are not allowed");
            return true;
        }
    }
}
#endif
