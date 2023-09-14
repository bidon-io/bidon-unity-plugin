#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System;
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

        private static readonly AndroidJavaClass LogLevelJClass;
        private static readonly AndroidJavaClass GenderJClass;
        private static readonly AndroidJavaClass GdprConsentStatusJClass;
        private static readonly AndroidJavaClass CoppaApplicabilityStatusJClass;

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

                LogLevelJClass = new AndroidJavaClass("org.bidon.sdk.logs.logging.Logger$Level");
                GenderJClass = new AndroidJavaClass("org.bidon.sdk.segment.models.Gender");
                GdprConsentStatusJClass = new AndroidJavaClass("org.bidon.sdk.regulation.Gdpr");
                CoppaApplicabilityStatusJClass = new AndroidJavaClass("org.bidon.sdk.regulation.Coppa");
            }
            catch (Exception e)
            {
                Debug.LogError($"BidonSdk operation is not possible due to incorrect integration: {e.Message}");
            }
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
                AdType = GetBidonAdType(ad.Call<AndroidJavaObject>("getDemandAd").Call<AndroidJavaObject>("getAdType")),
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

        private static BidonAdType GetBidonAdType(AndroidJavaObject adType)
        {
            if (adType == null) throw new ArgumentNullException(nameof(adType), "param can not be null");

            string javaAdType = adType.Call<string>("name");

            return javaAdType switch
            {
                "Banner" => BidonAdType.Banner,
                "Interstitial" => BidonAdType.Interstitial,
                "Rewarded" => BidonAdType.Rewarded,
                _ => throw new ArgumentOutOfRangeException(nameof(javaAdType), javaAdType, "value must be assignable to BidonAdType")
            };
        }

        private static BidonRevenuePrecision GetBidonRevenuePrecision(AndroidJavaObject precision)
        {
            if (precision == null) throw new ArgumentNullException(nameof(precision), "param can not be null");

            string javaPrecision = precision.Call<string>("name");

            return javaPrecision switch
            {
                "Precise" => BidonRevenuePrecision.Precise,
                "Estimated" => BidonRevenuePrecision.Estimated,
                _ => throw new ArgumentOutOfRangeException(nameof(javaPrecision), javaPrecision, "value must be assignable to BidonRevenuePrecision")
            };
        }

        public static IDictionary<string, object> GetDictionaryFromJavaMap(AndroidJavaObject jMap)
        {
            var outputDict = new Dictionary<string, object>();

            if (jMap == null) return outputDict;

            using var jList = new AndroidJavaObject("java.util.ArrayList", jMap.Call<AndroidJavaObject>("entrySet"));

            int countOfEntries = jList.Call<int>("size");
            for(int i = 0; i < countOfEntries; i++)
            {
                var jEntry = jList.Call<AndroidJavaObject>("get", i);
                outputDict.Add(jEntry.Call<string>("getKey"), GetCSharpObject(jEntry.Call<AndroidJavaObject>("getValue")));
            }
            return outputDict;
        }

        public static AndroidJavaObject GetLogLevelJavaObject(BidonLogLevel logLevel)
        {
            return logLevel switch
            {
                BidonLogLevel.Off => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Off"),
                BidonLogLevel.Error => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Error"),
                BidonLogLevel.Verbose => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Debug => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Info => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                BidonLogLevel.Warning => LogLevelJClass?.CallStatic<AndroidJavaObject>("valueOf", "Verbose"),
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
        }

        public static AndroidJavaObject GetGenderJavaObject(BidonUserGender gender)
        {
            return gender switch
            {
                BidonUserGender.Male => GenderJClass?.CallStatic<AndroidJavaObject>("valueOf", "Male"),
                BidonUserGender.Female => GenderJClass?.CallStatic<AndroidJavaObject>("valueOf", "Female"),
                BidonUserGender.Other => GenderJClass?.CallStatic<AndroidJavaObject>("valueOf", "Other"),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }

        public static AndroidJavaObject GetGdprConsentStatusJavaObject(BidonGdprConsentStatus consentStatus)
        {
            return consentStatus switch
            {
                BidonGdprConsentStatus.Unknown => GdprConsentStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "Unknown"),
                BidonGdprConsentStatus.Denied => GdprConsentStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "Denied"),
                BidonGdprConsentStatus.Given => GdprConsentStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "Given"),
                _ => throw new ArgumentOutOfRangeException(nameof(consentStatus), consentStatus, null)
            };
        }

        public static AndroidJavaObject GetCoppaApplicabilityStatusJavaObject(BidonCoppaApplicabilityStatus applicabilityStatus)
        {
            return applicabilityStatus switch
            {
                BidonCoppaApplicabilityStatus.Unknown => CoppaApplicabilityStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "Unknown"),
                BidonCoppaApplicabilityStatus.No => CoppaApplicabilityStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "No"),
                BidonCoppaApplicabilityStatus.Yes => CoppaApplicabilityStatusJClass?.CallStatic<AndroidJavaObject>("valueOf", "Yes"),
                _ => throw new ArgumentOutOfRangeException(nameof(applicabilityStatus), applicabilityStatus, null)
            };
        }

        private static object GetCSharpObject(AndroidJavaObject jObject)
        {
            using var boolJClass = new AndroidJavaClass("java.lang.Boolean");
            using var charJClass = new AndroidJavaClass("java.lang.Character");
            using var intJClass = new AndroidJavaClass("java.lang.Integer");
            using var longJClass = new AndroidJavaClass("java.lang.Long");
            using var floatJClass = new AndroidJavaClass("java.lang.Float");
            using var doubleJClass = new AndroidJavaClass("java.lang.Double");
            using var stringJClass = new AndroidJavaClass("java.lang.String");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), boolJClass.GetRawClass()))
                return jObject.Call<bool>("booleanValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), charJClass.GetRawClass()))
                return jObject.Call<char>("charValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), intJClass.GetRawClass()))
                return jObject.Call<int>("intValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), longJClass.GetRawClass()))
                return jObject.Call<long>("longValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), floatJClass.GetRawClass()))
                return jObject.Call<float>("floatValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), doubleJClass.GetRawClass()))
                return jObject.Call<double>("doubleValue");

            if (AndroidJNI.IsInstanceOf(jObject.GetRawObject(), stringJClass.GetRawClass()))
                return jObject.Call<string>("toString");

            throw new ArgumentException("Not supported type was detected");
        }

        public static object GetJavaObject(object value)
        {
            return value switch
            {
                bool _ => new AndroidJavaObject("java.lang.Boolean", value),
                char _ => new AndroidJavaObject("java.lang.Character", value),
                int _ => new AndroidJavaObject("java.lang.Integer", value),
                long _ => new AndroidJavaObject("java.lang.Long", value),
                float _ => new AndroidJavaObject("java.lang.Float", value),
                double _ => new AndroidJavaObject("java.lang.Double", value),
                string _ => value,
                _ => throw new ArgumentException("Incorrect type")
            };
        }
    }
}
#endif
