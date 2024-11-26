#if UNITY_IOS || BIDON_DEV_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonBannerSize
    {
        public int Width;
        public int Height;

        public BidonBannerSize ToBidonBannerSize()
        {
            return new BidonBannerSize
            {
                Width = Width,
                Height = Height,
            };
        }
    }
}
#endif
