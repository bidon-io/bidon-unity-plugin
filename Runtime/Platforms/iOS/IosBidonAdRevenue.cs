#if UNITY_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonAdRevenue
    {
        public double Revenue;
        public int RevenuePrecision;
        public string Currency;

        public BidonAdValue ToBidonAdValue()
        {
            return new BidonAdValue
            {
                AdRevenue = Revenue,
                CurrencyCode = Currency,
                RevenuePrecision = (BidonRevenuePrecision)RevenuePrecision,
            };
        }
    }
}
#endif
