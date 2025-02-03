#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidJavaObjectExtensions
    {
        public static bool IsValidInstanceOf(this AndroidJavaObject javaObject, AndroidJavaClass javaClass, bool logWhenInvalid = true)
        {
            if (javaObject == null || javaClass == null) return false;

            var javaObjectPtr = javaObject.GetRawObject();
            var javaClassPtr = javaClass.GetRawClass();
            if (javaObjectPtr == IntPtr.Zero || javaClassPtr == IntPtr.Zero) return false;

            bool isInstance = AndroidJNI.IsInstanceOf(javaObjectPtr, javaClassPtr);
            if (isInstance) return true;

            if (!logWhenInvalid) return false;

            Debug.LogError("[BidonPlugin] The parameter doesn't match the expected Java type. Bidon may not function properly");
#if BIDON_DEV
            throw new ArgumentException("The parameter doesn't match the expected Java type", nameof(javaObjectPtr));
#else
            return false;
#endif
        }

        public static void SafeCall(this AndroidJavaObject javaObject, string methodName, params object[] args)
        {
            if (javaObject == null || String.IsNullOrEmpty(methodName)) return;

            try
            {
                javaObject.Call(methodName, args);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to call static method '{methodName}'. Falling back to default value. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#endif
            }
        }

        public static T SafeCall<T>(this AndroidJavaObject javaObject, string methodName, params object[] args)
        {
            if (javaObject == null || String.IsNullOrEmpty(methodName)) return default;

            try
            {
                return javaObject.Call<T>(methodName, args);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to call method '{methodName}'. Falling back to default value. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return default;
#endif
            }
        }

        public static void SafeCallStatic(this AndroidJavaObject javaObject, string methodName, params object[] args)
        {
            if (javaObject == null || String.IsNullOrEmpty(methodName)) return;

            try
            {
                javaObject.CallStatic(methodName, args);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to call static method '{methodName}'. Falling back to default value. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#endif
            }
        }

        public static T SafeCallStatic<T>(this AndroidJavaObject javaObject, string methodName, params object[] args)
        {
            if (javaObject == null || String.IsNullOrEmpty(methodName)) return default;

            try
            {
                return javaObject.CallStatic<T>(methodName, args);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to call static method '{methodName}'. Falling back to default value. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return default;
#endif
            }
        }

        public static T SafeGetStatic<T>(this AndroidJavaObject javaObject, string fieldName)
        {
            if (javaObject == null || String.IsNullOrEmpty(fieldName)) return default;

            try
            {
                return javaObject.GetStatic<T>(fieldName);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to get static field '{fieldName}'. Falling back to default value. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return default;
#endif
            }
        }
    }
}
#endif
