#if UNITY_IOS || BIDON_DEV_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAdUnitInfoArray
    {
        public IntPtr values;
        public int length;

        public List<BidonAdUnitInfo> ToBidonAdUnitInfoList()
        {
            var adUnitInfoArray = new IosBidonAdUnitInfo[length];
            int sizeOfAdUnitInfo = Marshal.SizeOf(typeof(IosBidonAdUnitInfo));
            for (int i = 0; i < length; i++)
            {
                var currentPtr = IntPtr.Add(values, i * sizeOfAdUnitInfo);
                adUnitInfoArray[i] = Marshal.PtrToStructure<IosBidonAdUnitInfo>(currentPtr);
            }
            return adUnitInfoArray.Select(el => el.ToBidonAdUnitInfo()).ToList();
        }
    }
}
#endif
