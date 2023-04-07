#if UNITY_IOS

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal static class IosBidonHelper
    {
        public static BidonError GetBidonErrorFromInt(int cause)
        {
            BidonError error;
            switch (cause)
            {
                case 3:
                    error = BidonError.NoFill;
                    break;
                case 5:
                    error = BidonError.SdkNotInitialized;
                    break;
                case 7:
                    error = BidonError.NoContextFound;
                    break;
                default:
                    error = BidonError.Unspecified;
                    break;
            }

            return error;
        }
    }
}
#endif
