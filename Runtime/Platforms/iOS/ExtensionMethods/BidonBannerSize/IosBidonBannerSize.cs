#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct IosBidonBannerSize
    {
        public int Width;
        public int Height;
    }
}
#endif
