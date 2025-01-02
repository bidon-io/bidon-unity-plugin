#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal static class IosBidonAdRevenueConverter
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginHelperFreeAdRevenue")]
        private static extern void BidonAdRevenueFree(IntPtr ptr);

        private static void FreeAdRevenue(IntPtr ptr) => BidonAdRevenueFree(ptr);

        public static BidonAdValue ToBidonAdValue(this IntPtr ptr)
        {
            var iosBidonAdRevenue = IosBidonMarshal.SafePtrToStructure<IosBidonAdRevenue>(ptr);
            var bidonAdValue = iosBidonAdRevenue?.ToBidonAdValue();
            FreeAdRevenue(ptr);
            return bidonAdValue;
        }

        private static BidonAdValue ToBidonAdValue(this IosBidonAdRevenue iosBidonAdRevenue)
        {
            return new BidonAdValue
            {
                AdRevenue = iosBidonAdRevenue.Revenue,
                CurrencyCode = iosBidonAdRevenue.Currency,
                RevenuePrecision = iosBidonAdRevenue.RevenuePrecision.ToNullableEnum<BidonRevenuePrecision>(),
            };
        }
    }
}
#endif
