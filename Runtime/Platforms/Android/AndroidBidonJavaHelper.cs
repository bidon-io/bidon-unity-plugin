#if UNITY_ANDROID
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal static class AndroidBidonJavaHelper
    {
        private static readonly AndroidJavaClass SdkNotInitializedJClass;
        private static readonly AndroidJavaClass AppKeyIsInvalidJClass;
        private static readonly AndroidJavaClass InternalServerSdkErrorJClass;
        private static readonly AndroidJavaClass NetworkErrorJClass;
        private static readonly AndroidJavaClass AuctionInProgressJClass;
        private static readonly AndroidJavaClass NoAuctionResultsJClass;
        private static readonly AndroidJavaClass NoRoundResultsJClass;
        private static readonly AndroidJavaClass NoContextFoundJClass;
        private static readonly AndroidJavaClass NoBidJClass;
        private static readonly AndroidJavaClass NoFillJClass;
        private static readonly AndroidJavaClass BidTimedOutJClass;
        private static readonly AndroidJavaClass FillTimedOutJClass;
        private static readonly AndroidJavaClass AdFormatIsNotSupportedJClass;
        private static readonly AndroidJavaClass FullscreenAdNotReadyJClass;
        private static readonly AndroidJavaClass NoAppropriateAdUnitIdJClass;
        private static readonly AndroidJavaClass ExpiredJClass;

        static AndroidBidonJavaHelper()
        {
            try
            {
                SdkNotInitializedJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$SdkNotInitialized");
                AppKeyIsInvalidJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$AppKeyIsInvalid");
                InternalServerSdkErrorJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$InternalServerSdkError");
                NetworkErrorJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NetworkError");
                AuctionInProgressJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$AuctionInProgress");
                NoAuctionResultsJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoAuctionResults");
                NoRoundResultsJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoRoundResults");
                NoContextFoundJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoContextFound");
                NoBidJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoBid");
                NoFillJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoFill");
                BidTimedOutJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$BidTimedOut");
                FillTimedOutJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$FillTimedOut");
                AdFormatIsNotSupportedJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$AdFormatIsNotSupported");
                FullscreenAdNotReadyJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$FullscreenAdNotReady");
                NoAppropriateAdUnitIdJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$NoAppropriateAdUnitId");
                ExpiredJClass = new AndroidJavaClass("org.bidon.sdk.config.BidonError$Expired");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
            }
        }

        public static IEnumerable<BidonAuctionResult> GetListOfBidonAuctionResults(AndroidJavaObject list)
        {
            if (list == null) return Enumerable.Empty<BidonAuctionResult>();

            int countOfElements = list.Call<int>("size");
            var resultList = new List<BidonAuctionResult>();
            for(int i = 0; i < countOfElements; i++)
            {
                resultList.Add(GetBidonAuctionResult(list.Call<AndroidJavaObject>("get", i)));
            }
            return resultList;
        }

        public static BidonAdValue GetBidonAdValue(AndroidJavaObject adValue)
        {
            if (adValue == null) return null;

            return new BidonAdValue
            {
                AdRevenue = adValue.Call<double>("getAdRevenue"),
                CurrencyCode = adValue.Call<string>("getCurrency"),
                RevenuePrecision = GetBidonRevenuePrecision(adValue.Call<AndroidJavaObject>("getPrecision"))
            };
        }

        public static BidonAd GetBidonAd(AndroidJavaObject ad)
        {
            if (ad == null) return null;

            return new BidonAd
            {
                AdUnitId = ad.Call<string>("getAdUnitId"),
                AuctionId = ad.Call<string>("getAuctionId"),
                CurrencyCode = ad.Call<string>("getCurrencyCode"),
                DemandAd = GetBidonDemandAd(ad.Call<AndroidJavaObject>("getDemandAd")),
                Dsp = ad.Call<string>("getDsp"),
                Ecpm = ad.Call<double>("getEcpm"),
                NetworkName = ad.Call<string>("getNetworkName"),
                RoundId = ad.Call<string>("getRoundId")
            };
        }

        public static BidonReward GetBidonReward(AndroidJavaObject reward)
        {
            if (reward == null) return null;

            return new BidonReward
            {
                Amount = reward.Call<int>("getAmount"),
                Label = reward.Call<string>("getLabel")
            };
        }

        public static BidonError GetBidonError(AndroidJavaObject cause)
        {
            if (cause == null) return BidonError.Unspecified;

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), SdkNotInitializedJClass.GetRawClass()))
            {
                return BidonError.SdkNotInitialized;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), AppKeyIsInvalidJClass.GetRawClass()))
            {
                return BidonError.AppKeyIsInvalid;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), InternalServerSdkErrorJClass.GetRawClass()))
            {
                return BidonError.InternalServerSdkError;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NetworkErrorJClass.GetRawClass()))
            {
                return BidonError.NetworkError;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), AuctionInProgressJClass.GetRawClass()))
            {
                return BidonError.AuctionInProgress;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoAuctionResultsJClass.GetRawClass()))
            {
                return BidonError.NoAuctionResults;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoRoundResultsJClass.GetRawClass()))
            {
                return BidonError.NoRoundResults;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoContextFoundJClass.GetRawClass()))
            {
                return BidonError.NoContextFound;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoBidJClass.GetRawClass()))
            {
                return BidonError.NoBid;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoFillJClass.GetRawClass()))
            {
                return BidonError.NoFill;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), BidTimedOutJClass.GetRawClass()))
            {
                return BidonError.BidTimedOut;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), FillTimedOutJClass.GetRawClass()))
            {
                return BidonError.FillTimedOut;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), AdFormatIsNotSupportedJClass.GetRawClass()))
            {
                return BidonError.AdFormatIsNotSupported;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), FullscreenAdNotReadyJClass.GetRawClass()))
            {
                return BidonError.FullscreenAdNotReady;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), NoAppropriateAdUnitIdJClass.GetRawClass()))
            {
                return BidonError.NoAppropriateAdUnitId;
            }

            if (AndroidJNI.IsInstanceOf(cause.GetRawObject(), ExpiredJClass.GetRawClass()))
            {
                return BidonError.Expired;
            }

            return BidonError.Unspecified;
        }

        private static BidonAuctionResult GetBidonAuctionResult(AndroidJavaObject result)
        {
            if (result == null) return null;

            return new BidonAuctionResult
            {
                AdSource = GetBidonAdSource(result.Call<AndroidJavaObject>("getAdSource")),
                Ecpm = result.Call<double>("getEcpm")
            };
        }

        private static BidonAdSource GetBidonAdSource(AndroidJavaObject adSource)
        {
            if (adSource == null) return null;

            return new BidonAdSource
            {
                Ad = GetBidonAd(adSource.Call<AndroidJavaObject>("getAd")),
                DemandId = adSource.Call<AndroidJavaObject>("getDemandId").Call<string>("getDemandId"),
                IsReadyToShow = adSource.Call<bool>("isAdReadyToShow")
            };
        }

        private static BidonDemandAd GetBidonDemandAd(AndroidJavaObject demandAd)
        {
            if (demandAd == null) return null;

            return new BidonDemandAd
            {
                AdType = GetBidonAdType(demandAd.Call<AndroidJavaObject>("getAdType")),
                Placement = demandAd.Call<string>("getPlacement")
            };
        }

        private static BidonAdType GetBidonAdType(AndroidJavaObject adType)
        {
            if (adType == null) throw new ArgumentNullException(nameof(adType), "param can not be null");

            string javaAdType = adType.Call<string>("name");

            BidonAdType bidonAdType;
            switch (javaAdType)
            {
                case "Banner":
                    bidonAdType = BidonAdType.Banner;
                    break;
                case "Interstitial":
                    bidonAdType = BidonAdType.Interstitial;
                    break;
                case "Rewarded":
                    bidonAdType = BidonAdType.Rewarded;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(javaAdType), javaAdType, "value must be assignable to BidonAdType");
            }

            return bidonAdType;
        }

        private static BidonRevenuePrecision GetBidonRevenuePrecision(AndroidJavaObject precision)
        {
            if (precision == null) throw new ArgumentNullException(nameof(precision), "param can not be null");

            string javaPrecision = precision.Call<string>("name");

            BidonRevenuePrecision revenuePrecision;
            switch (javaPrecision)
            {
                case "Precise":
                    revenuePrecision = BidonRevenuePrecision.Precise;
                    break;
                case "Estimated":
                    revenuePrecision = BidonRevenuePrecision.Estimated;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(javaPrecision), javaPrecision, "value must be assignable to BidonRevenuePrecision");
            }

            return revenuePrecision;
        }
    }
}
#endif
