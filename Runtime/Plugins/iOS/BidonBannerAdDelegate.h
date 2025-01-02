//
//  BidonBannerAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUnityPluginTypes.h>

@interface BDNUnityPluginBannerAdDelegate : NSObject <BDNAdObjectDelegate>

@property (nonatomic, assign) DidLoad              bannerDidLoadCallback;
@property (nonatomic, assign) DidFailToLoad        bannerDidFailToLoadCallback;
@property (nonatomic, assign) DidRecordImpression  bannerDidRecordImpressionCallback;
@property (nonatomic, assign) DidFailToPresent     bannerDidFailToPresentCallback;
@property (nonatomic, assign) DidClick             bannerDidClickCallback;
@property (nonatomic, assign) DidExpire            bannerDidExpireCallback;
@property (nonatomic, assign) DidPayRevenue        bannerDidPayRevenueCallback;

@end
