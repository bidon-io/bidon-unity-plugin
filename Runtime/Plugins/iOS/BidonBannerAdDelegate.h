//
//  BidonBannerAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginStructs.h>

typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*DidRecordImpression)(BDNUnityPluginAd* ad);
typedef void (*DidFailToPresent)(int error);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidExpire)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);

@interface BDNUnityPluginBannerAdDelegate : NSObject <BDNAdObjectDelegate>

@property (assign) DidLoad              bannerDidLoadCallback;
@property (assign) DidFailToLoad        bannerDidFailToLoadCallback;
@property (assign) DidRecordImpression  bannerDidRecordImpressionCallback;
@property (assign) DidFailToPresent     bannerDidFailToPresentCallback;
@property (assign) DidClick             bannerDidClickCallback;
@property (assign) DidExpire            bannerDidExpireCallback;
@property (assign) DidPayRevenue        bannerDidPayRevenueCallback;

@end
