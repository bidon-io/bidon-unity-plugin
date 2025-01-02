#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal static class IosBidonBannerSizeConverter
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginHelperFreeBannerSize")]
        private static extern void BidonBannerSizeFree(IntPtr ptr);

        private static void FreeBannerSize(IntPtr ptr) => BidonBannerSizeFree(ptr);

        public static BidonBannerSize ToBidonBannerSize(this IntPtr ptr)
        {
            var iosBidonBannerSize = IosBidonMarshal.SafePtrToStructure<IosBidonBannerSize>(ptr);
            var bidonBannerSize = iosBidonBannerSize?.ToBidonBannerSize();
            FreeBannerSize(ptr);
            return bidonBannerSize;
        }

        private static BidonBannerSize ToBidonBannerSize(this IosBidonBannerSize iosBidonBannerSize)
        {
            return new BidonBannerSize
            {
                Width = iosBidonBannerSize.Width,
                Height = iosBidonBannerSize.Height,
            };
        }
    }
}
#endif
