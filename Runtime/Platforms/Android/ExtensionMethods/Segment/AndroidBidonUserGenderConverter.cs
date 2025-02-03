#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonUserGenderConverter
    {
        private static readonly AndroidJavaClass GenderJClass;

        static AndroidBidonUserGenderConverter()
        {
            GenderJClass = AndroidBidonFactory.SafeCreateJavaClass("org.bidon.sdk.segment.models.Gender");
        }

        public static BidonUserGender? ToBidonUserGender(this AndroidJavaObject gender)
        {
            if (!gender.IsValidInstanceOf(GenderJClass)) return null;

            string genderStr = gender.SafeCall<string>("name")?.ToLower();

            return genderStr switch
            {
                "male" => BidonUserGender.Male,
                "female" => BidonUserGender.Female,
                "other" => BidonUserGender.Other,
#if BIDON_DEV
                _ => throw new ArgumentOutOfRangeException(nameof(genderStr), genderStr, $"Unsupported value received. It cannot be mapped to '{nameof(BidonUserGender)}'")
#else
                _ => null
#endif
            };
        }

        public static AndroidJavaObject ToJavaObject(this BidonUserGender? gender)
        {
            if (gender == null) return null;

            if (!Enum.IsDefined(typeof(BidonUserGender), gender)) LogErrorAndReturnDefault();

            return gender switch
            {
                BidonUserGender.Male => GenderJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Male"),
                BidonUserGender.Female => GenderJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Female"),
                BidonUserGender.Other => GenderJClass.SafeCallStatic<AndroidJavaObject>("valueOf", "Other"),
                _ => LogErrorAndReturnDefault()
            };

            AndroidJavaObject LogErrorAndReturnDefault()
            {
                Debug.LogError("[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: 'null'");
#if BIDON_DEV
                throw new ArgumentOutOfRangeException(nameof(gender), gender, $"Unsupported value received. It cannot be mapped to '{nameof(BidonUserGender)}'");
#else
                return null;
#endif
            }
        }
    }
}
#endif
