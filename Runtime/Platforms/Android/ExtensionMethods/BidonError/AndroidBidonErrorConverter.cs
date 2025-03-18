#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidBidonErrorConverter
    {
        private static readonly Dictionary<string, BidonError> JavaClassToBidonErrorMap;

        static AndroidBidonErrorConverter()
        {
            JavaClassToBidonErrorMap = new Dictionary<string, BidonError>(18)
            {
                { "org.bidon.sdk.config.BidonError$SdkNotInitialized", BidonError.SdkNotInitialized },
                { "org.bidon.sdk.config.BidonError$AppKeyIsInvalid", BidonError.AppKeyIsInvalid },
                { "org.bidon.sdk.config.BidonError$InternalServerSdkError", BidonError.InternalServerSdkError },
                { "org.bidon.sdk.config.BidonError$NetworkError", BidonError.NetworkError },
                { "org.bidon.sdk.config.BidonError$AuctionInProgress", BidonError.AuctionInProgress },
                { "org.bidon.sdk.config.BidonError$AuctionCancelled", BidonError.AuctionCancelled },
                { "org.bidon.sdk.config.BidonError$NoAuctionResults", BidonError.NoAuctionResults },
                { "org.bidon.sdk.config.BidonError$NoRoundResults", BidonError.NoRoundResults },
                { "org.bidon.sdk.config.BidonError$NoContextFound", BidonError.NoContextFound },
                { "org.bidon.sdk.config.BidonError$NoBid", BidonError.NoBid },
                { "org.bidon.sdk.config.BidonError$NoFill", BidonError.NoFill },
                { "org.bidon.sdk.config.BidonError$BidTimedOut", BidonError.BidTimedOut },
                { "org.bidon.sdk.config.BidonError$FillTimedOut", BidonError.FillTimedOut },
                { "org.bidon.sdk.config.BidonError$AdFormatIsNotSupported", BidonError.AdFormatIsNotSupported },
                { "org.bidon.sdk.config.BidonError$Unspecified", BidonError.Unspecified },
                { "org.bidon.sdk.config.BidonError$AdNotReady", BidonError.AdNotReady },
                { "org.bidon.sdk.config.BidonError$NoAppropriateAdUnitId", BidonError.NoAppropriateAdUnitId },
                { "org.bidon.sdk.config.BidonError$Expired", BidonError.Expired },
                { "org.bidon.sdk.config.BidonError$IncorrectAdUnit", BidonError.IncorrectAdUnit },
            };
        }

        public static BidonError ToBidonError(this AndroidJavaObject cause)
        {
            if (cause == null || (JavaClassToBidonErrorMap?.Count ?? 0) == 0) return BidonError.Unspecified;

            foreach (var error in JavaClassToBidonErrorMap)
            {
                using var javaClass = AndroidBidonFactory.SafeCreateJavaClass(error.Key);
                if (cause.IsValidInstanceOf(javaClass, false)) return error.Value;
            }
#if BIDON_DEV
            throw new System.ArgumentException("Unexpected Java type was encountered");
#else
            return BidonError.Unspecified;
#endif
        }
    }
}
#endif
