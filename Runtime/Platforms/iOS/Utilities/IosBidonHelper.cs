#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;

namespace Bidon.Mediation
{
    internal static class IosBidonHelper
    {
        public static IDictionary<string, object> GetDictionaryFromJsonString(string jsonString)
        {
            var outputDict = new Dictionary<string, object>();

            if (String.IsNullOrEmpty(jsonString)) return outputDict;

            jsonString = jsonString
                .Replace("{", String.Empty)
                .Replace("}", String.Empty)
                .Replace("\"", String.Empty);

            string[] items = jsonString.Split(',');

            foreach (string item in items)
            {
                string[] kvp = item.Split(':');
                if (kvp.Length != 2) continue;

                if (Boolean.TryParse(kvp[1], out bool valueBool))
                {
                    outputDict.Add(kvp[0], valueBool);
                }
                else if (Char.TryParse(kvp[1], out char valueChar))
                {
                    outputDict.Add(kvp[0], valueChar);
                }
                else if (Int32.TryParse(kvp[1], out int valueInt))
                {
                    outputDict.Add(kvp[0], valueInt);
                }
                else if (Int64.TryParse(kvp[1], out long valueLong))
                {
                    outputDict.Add(kvp[0], valueLong);
                }
                else if (Single.TryParse(kvp[1].Replace('.', ','), out float valueFloat)
                         && !Single.IsNaN(valueFloat)
                         && !Single.IsInfinity(valueFloat))
                {
                    outputDict.Add(kvp[0], valueFloat);
                }
                else if (Double.TryParse(kvp[1].Replace('.', ','), out double valueDouble)
                         && !Double.IsNaN(valueDouble)
                         && !Double.IsInfinity(valueDouble))
                {
                    outputDict.Add(kvp[0], valueDouble);
                }
                else
                {
                    outputDict.Add(kvp[0], kvp[1]);
                }
            }
            return outputDict;
        }
    }
}
#endif
