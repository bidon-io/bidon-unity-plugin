#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonFactory
    {
        public static AndroidJavaClass SafeCreateJavaClass(string className)
        {
            try
            {
                return new AndroidJavaClass(className);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to load class '{className}'. Bidon may not function properly. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return null;
#endif
            }
        }

        public static AndroidJavaObject SafeCreateJavaObject(string className, params object[] args)
        {
            try
            {
                return new AndroidJavaObject(className, args);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to create instance of '{className}'. Bidon may not function properly. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return null;
#endif
            }
        }

        public static AndroidJavaObject SafeGetCurrentActivityJavaObject()
        {
            return SafeCreateJavaClass("com.unity3d.player.UnityPlayer").SafeGetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
#endif
