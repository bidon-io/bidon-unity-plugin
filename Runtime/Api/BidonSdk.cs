// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class BidonSdk
    {
        #region Singleton

        private static IBidonSdk _instance;

        private static readonly object Lock = new object();

        public static IBidonSdk Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BidonSdkClient();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        public static string PluginVersion => BidonConstants.PluginVersion;
    }
}
