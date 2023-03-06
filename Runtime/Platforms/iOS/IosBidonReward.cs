#if UNITY_IOS
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IosBidonReward
    {
        public string Label;
        public double Amount;

        public BidonReward ToBidonReward()
        {
            return new BidonReward
            {
                Label = Label,
                Amount = (int)Amount,
            };
        }
    }
}
#endif
