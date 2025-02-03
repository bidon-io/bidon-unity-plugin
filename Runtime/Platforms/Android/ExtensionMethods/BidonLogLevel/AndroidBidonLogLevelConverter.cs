#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonLogLevelConverter
    {
        private static readonly AndroidJavaClass LoggerLevelJClass;

        static AndroidBidonLogLevelConverter()
        {
            LoggerLevelJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.logs.logging.Logger$Level");
        }

        public static BidonLogLevel? ToBidonLogLevel(this AndroidJavaObject logLevel)
        {
            if (!logLevel.IsValidInstanceOf(LoggerLevelJClass)) return null;

            string logLevelStr = logLevel.SafeCall<string>("name")?.ToLower();

            return logLevelStr switch
            {
                "off" => BidonLogLevel.Off,
                "error" => BidonLogLevel.Error,
                "verbose" => BidonLogLevel.Verbose,
#if BIDON_DEV
                _ => throw new ArgumentOutOfRangeException(nameof(logLevelStr), logLevelStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonLogLevel)}'")
#else
                _ => null
#endif
            };
        }

        public static AndroidJavaObject ToJavaObject(this BidonLogLevel logLevel)
        {
            if (!Enum.IsDefined(typeof(BidonLogLevel), logLevel)) LogErrorAndReturnDefault();

            return logLevel switch
            {
                BidonLogLevel.Off => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Off"),
                BidonLogLevel.Error => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Error"),
                BidonLogLevel.Verbose => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Debug => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Info => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Warning => LoggerLevelJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: '{nameof(BidonLogLevel.Off)}'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, $"Unsupported value received. It cannot be mapped to '{nameof(BidonLogLevel)}'");
#else
                return BidonLogLevel.Off.ToJavaObject();
#endif
            }
        }
    }
}
#endif
