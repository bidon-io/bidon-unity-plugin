//
//  BidonUnityPluginTypes.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 20/09/2023.
//

typedef struct {
    const char* Uid;
    const char* DemandId;
    const char* Label;
    double      PriceFloor;
    int         BidType;
    const char* Extras;
} BDNUnityPluginAdUnit;

typedef struct {
    BDNUnityPluginAdUnit* AdUnit;
    const char*           AuctionId;
    const char*           CurrencyCode;
    int                   AdType;
    const char*           Dsp;
    double                Price;
    const char*           NetworkName;
} BDNUnityPluginAd;

typedef struct {
    const char* DemandId;
    const char* Label;
    const char* Price; // double or NULL
    const char* Uid;
    const char* BidType;
    const char* FillStartTs; // long or NULL
    const char* FillFinishTs; // long or NULL
    const char* Status;
    const char* Ext;
} BDNUnityPluginAdUnitInfo;

typedef struct {
    BDNUnityPluginAdUnitInfo** Values;
    int                        Length;
} BDNUnityPluginAdUnitInfoArray;

typedef struct {
    const char*                    AuctionId;
    const char*                    AuctionConfigurationId; // long or NULL
    const char*                    AuctionConfigurationUid;
    long                           AuctionTimeout;
    double                         AuctionPriceFloor;
    BDNUnityPluginAdUnitInfoArray* NoBids;
    BDNUnityPluginAdUnitInfoArray* AdUnits;
} BDNUnityPluginAuctionInfo;

typedef struct {
    double      Revenue;
    int         RevenuePrecision;
    const char* Currency;
} BDNUnityPluginAdRevenue;

typedef struct {
    const char* Label;
    double      Amount;
} BDNUnityPluginReward;

typedef struct {
    int Width;
    int Height;
} BDNUnityPluginBannerSize;

typedef const void* CFBDNUnityPluginBannerAdRef;
typedef const void* CFBDNUnityPluginBannerAdDelegateRef;

typedef const void* CFBDNUnityPluginInterstitialAdRef;
typedef const void* CFBDNUnityPluginInterstitialAdDelegateRef;

typedef const void* CFBDNUnityPluginRewardedAdRef;
typedef const void* CFBDNUnityPluginRewardedAdDelegateRef;

typedef void (*InitializationFinishedCallback)();

typedef void (*DidLoad)(BDNUnityPluginAd* ad, BDNUnityPluginAuctionInfo* auctionInfo);
typedef void (*DidFailToLoad)(BDNUnityPluginAuctionInfo* auctionInfo, int error);
typedef void (*DidFailToPresent)(int error);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidExpire)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* adRevenue);

typedef void (*DidRecordImpression)(BDNUnityPluginAd* ad);

typedef void (*WillPresent)(BDNUnityPluginAd* ad);
typedef void (*DidHide)(BDNUnityPluginAd* ad);

typedef void (*DidReceiveReward)(BDNUnityPluginAd* ad, BDNUnityPluginReward* reward);
