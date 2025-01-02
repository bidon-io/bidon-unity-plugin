#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bidon.Mediation
{
    internal static class IosBidonAdUnitInfoArrayConverter
    {
        public static IEnumerable<BidonAdUnitInfo> ToBidonAdUnitInfoList(this IntPtr ptr)
        {
            var iosBidonAdUnitInfoArray = IosBidonMarshal.SafePtrToStructure<IosBidonAdUnitInfoArray>(ptr);
            var bidonAdUnitInfoList = iosBidonAdUnitInfoArray?.ToBidonAdUnitInfoList() ?? Enumerable.Empty<BidonAdUnitInfo>();
            return bidonAdUnitInfoList;
        }

        private static List<BidonAdUnitInfo> ToBidonAdUnitInfoList(this IosBidonAdUnitInfoArray iosBidonAdUnitInfoArray)
        {
            var adUnitInfoList = new List<BidonAdUnitInfo>(iosBidonAdUnitInfoArray.Length);

            for (int i = 0; i < iosBidonAdUnitInfoArray.Length; i++)
            {
                var currentPtr = IosBidonMarshal.SafeReadIntPtr(iosBidonAdUnitInfoArray.ValuesPtr, i * IntPtr.Size);
                if (currentPtr == IntPtr.Zero) continue;
                var adUnitInfo = currentPtr.ToBidonAdUnitInfo();
                if (adUnitInfo != null) adUnitInfoList.Add(adUnitInfo);
            }

            return adUnitInfoList;
        }
    }
}
#endif
