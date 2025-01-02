#if UNITY_IOS || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Runtime.InteropServices;

namespace Bidon.Mediation
{
    internal static class IosBidonRewardConverter
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginHelperFreeReward")]
        private static extern void BidonRewardFree(IntPtr ptr);

        private static void FreeReward(IntPtr ptr) => BidonRewardFree(ptr);

        public static BidonReward ToBidonReward(this IntPtr ptr)
        {
            var iosBidonReward = IosBidonMarshal.SafePtrToStructure<IosBidonReward>(ptr);
            var bidonReward = iosBidonReward?.ToBidonReward();
            FreeReward(ptr);
            return bidonReward;
        }

        private static BidonReward ToBidonReward(this IosBidonReward iosBidonReward)
        {
            return new BidonReward
            {
                Label = iosBidonReward.Label,
                Amount = iosBidonReward.Amount,
            };
        }
    }
}
#endif
