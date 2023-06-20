#if UNITY_IOS || BIDON_DEV_IOS
using System;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal static class IosBidonHelper
    {
        public static BidonError GetBidonErrorFromInt(int cause)
        {
            if (Enum.IsDefined(typeof(BidonError), cause + 1))
            {
                return (BidonError)(cause + 1);
            }

            return BidonError.Unspecified;
        }
    }
}
#endif
