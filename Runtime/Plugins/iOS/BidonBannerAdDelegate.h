//
//  BidonBannerAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginTypes.h>

@interface BDNUnityPluginBannerAdDelegate : NSObject <BDNAdObjectDelegate>

@property (assign) DidLoad              bannerDidLoadCallback;
@property (assign) DidFailToLoad        bannerDidFailToLoadCallback;
@property (assign) DidRecordImpression  bannerDidRecordImpressionCallback;
@property (assign) DidFailToPresent     bannerDidFailToPresentCallback;
@property (assign) DidClick             bannerDidClickCallback;
@property (assign) DidExpire            bannerDidExpireCallback;
@property (assign) DidPayRevenue        bannerDidPayRevenueCallback;

@end
