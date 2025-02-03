#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonJavaHelper
    {
        public static object GetJavaObject(object value)
        {
            if (value == null) return null;

            try
            {
                return value switch
                {
                    bool b => new AndroidJavaObject("java.lang.Boolean", b),
                    char c => new AndroidJavaObject("java.lang.Character", c),
                    int i => new AndroidJavaObject("java.lang.Integer", i),
                    long l => new AndroidJavaObject("java.lang.Long", l),
                    float f => new AndroidJavaObject("java.lang.Float", f),
                    double d => new AndroidJavaObject("java.lang.Double", d),
                    string s => s,
                    _ => throw new ArgumentException($"Conversion of '{value.GetType()}' type to Java is not implemented")
                };
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to convert C# type to Java. Falling back to default value: 'null'. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return null;
#endif
            }
        }

        public static IDictionary<string, object> GetDictionaryFromJavaMap(AndroidJavaObject jMap)
        {
            var outputDict = new Dictionary<string, object>();

            if (jMap == null) return outputDict;

            try
            {
                using var jList = new AndroidJavaObject("java.util.ArrayList", jMap.Call<AndroidJavaObject>("entrySet"));

                int countOfEntries = jList.Call<int>("size");
                for (int i = 0; i < countOfEntries; i++)
                {
                    var jEntry = jList.Call<AndroidJavaObject>("get", i);
                    outputDict.Add(jEntry.Call<string>("getKey"), GetCSharpObject(jEntry.Call<AndroidJavaObject>("getValue")));
                }

                return outputDict;
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to convert Java map to C# dictionary. Falling back to default value: 'empty dictionary'. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return outputDict;
#endif
            }
        }

        private static object GetCSharpObject(AndroidJavaObject jObject)
        {
            if (jObject == null) return null;

            try
            {
                var typeMappings = new Dictionary<string, Func<AndroidJavaObject, object>>(7)
                {
                    { "java.lang.Boolean", obj => obj.Call<bool>("booleanValue") },
                    { "java.lang.Character", obj => obj.Call<char>("charValue") },
                    { "java.lang.Integer", obj => obj.Call<int>("intValue") },
                    { "java.lang.Long", obj => obj.Call<long>("longValue") },
                    { "java.lang.Float", obj => obj.Call<float>("floatValue") },
                    { "java.lang.Double", obj => obj.Call<double>("doubleValue") },
                    { "java.lang.String", obj => obj.Call<string>("toString") },
                };

                foreach (var mapping in typeMappings)
                {
                    using var javaClass = AndroidBidonFactory.SafeCreateJavaClass(mapping.Key);
                    if (jObject.IsValidInstanceOf(javaClass, false)) return mapping.Value(jObject);
                }

                throw new ArgumentException("Unexpected Java type was encountered");
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to convert Java type to C#. Falling back to default value: 'null'. Exception: {e.Message}");
#if BIDON_DEV
                throw;
#else
                return null;
#endif
            }
        }
    }
}
#endif
