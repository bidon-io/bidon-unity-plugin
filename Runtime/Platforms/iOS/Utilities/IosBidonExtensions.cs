#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class IosBidonExtensions
    {
        public static T ToEnum<T>(this int value, T fallbackValue) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), value)) return (T)(ValueType)value;

            Debug.LogError($"[BidonPlugin] Failed to map {nameof(value)}={value} to '{nameof(T)}'. Falling back to '{fallbackValue}'");
#if BIDON_DEV
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Unsupported value received. It cannot be mapped to '{nameof(T)}'");
#else
            return fallbackValue;
#endif
        }

        public static T? ToNullableEnum<T>(this int value) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), value)) return (T)(ValueType)value;

            Debug.LogError($"[BidonPlugin] Failed to map {nameof(value)}={value} to '{nameof(T)}'. Falling back to 'null'");
#if BIDON_DEV
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Unsupported value received. It cannot be mapped to '{nameof(T)}'");
#else
            return null;
#endif
        }

        public static long? ToNullableLong(this string value)
        {
            return Int64.TryParse(value, out long result) ? result : (long?)null;
        }

        public static double? ToNullableDouble(this string value)
        {
            return Double.TryParse(value, out double result) ? result : (double?)null;
        }
    }
}
#endif
