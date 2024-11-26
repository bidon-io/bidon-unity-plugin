//
//  BidonUnityPluginTypes.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 20/09/20233.
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
    BDNUnityPluginAdUnit AdUnit;
    const char*          AuctionId;
    const char*          CurrencyCode;
    int                  AdType;
    double               Ecpm;
    const char*          NetworkName;
} BDNUnityPluginAd;

typedef struct {
    double      Revenue;
    int         RevenuePrecision;
    const char* Currency;
} BDNUnityPluginAdRevenue;

typedef struct {
    const char* label;
    double      amount;
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

typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*DidFailToPresent)(int error);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidExpire)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);

typedef void (*DidRecordImpression)(BDNUnityPluginAd* ad);

typedef void (*WillPresent)(BDNUnityPluginAd* ad);
typedef void (*DidHide)(BDNUnityPluginAd* ad);

typedef void (*DidReceiveReward)(BDNUnityPluginAd* ad, BDNUnityPluginReward* reward);
