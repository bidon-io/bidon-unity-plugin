#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonAdUnitInfoListConverter
    {
        private static readonly AndroidJavaClass ListJClass;

        static AndroidBidonAdUnitInfoListConverter()
        {
            ListJClass = AndroidBidonFactory.SafeCreateJavaClass("java.util.List");
        }

        public static List<BidonAdUnitInfo> ToBidonAdUnitInfoList(this AndroidJavaObject list)
        {
            var outputList = new List<BidonAdUnitInfo>();

            if (!list.IsValidInstanceOf(ListJClass)) return outputList;

            int countOfEntries = list.SafeCall<int>("size");
            for (int i = 0; i < countOfEntries; i++)
            {
                var jEntry = list.SafeCall<AndroidJavaObject>("get", i);
                var adUnitInfo = jEntry.ToBidonAdUnitInfo();
                if (adUnitInfo != null) outputList.Add(adUnitInfo);
            }

            return outputList;
        }
    }
}
#endif
