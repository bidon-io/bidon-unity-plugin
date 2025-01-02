#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal static class IosBidonAdConverter
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginHelperFreeAd")]
        private static extern void BidonAdFree(IntPtr ptr);

        private static void FreeAd(IntPtr ptr) => BidonAdFree(ptr);

        public static BidonAd ToBidonAd(this IntPtr ptr)
        {
            var iosBidonAd = IosBidonMarshal.SafePtrToStructure<IosBidonAd>(ptr);
            var bidonAd = iosBidonAd?.ToBidonAd();
            FreeAd(ptr);
            return bidonAd;
        }

        private static BidonAd ToBidonAd(this IosBidonAd iosBidonAd)
        {
            return new BidonAd
            {
                AdUnit = iosBidonAd.AdUnitPtr.ToBidonAdUnit(),
                AuctionId = iosBidonAd.AuctionId,
                CurrencyCode = iosBidonAd.CurrencyCode,
                AdType = iosBidonAd.AdType.ToNullableEnum<BidonAdType>(),
                Dsp = iosBidonAd.Dsp,
                Price = iosBidonAd.Price,
                NetworkName = iosBidonAd.NetworkName,
            };
        }
    }
}
#endif
