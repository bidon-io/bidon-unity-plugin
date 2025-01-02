#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class IosBidonMarshal
    {
        public static T? SafePtrToStructure<T>(IntPtr ptr) where T : struct
        {
            if (ptr == IntPtr.Zero) return null;

            try
            {
                return Marshal.PtrToStructure<T>(ptr);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to marshal data to '{typeof(T)}'. Falling back to default value: 'null'. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return null;
#endif
            }
        }

        public static IntPtr SafeReadIntPtr(IntPtr ptr, int offset)
        {
            try
            {
                return Marshal.ReadIntPtr(ptr, offset);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to read a processor native sized integer. Falling back to default value: '{nameof(IntPtr.Zero)}'. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return IntPtr.Zero;
#endif
            }
        }
    }
}
#endif
