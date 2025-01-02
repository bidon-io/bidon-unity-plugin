//
//  BidonRewardedAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUnityPluginTypes.h>

@interface BDNUnityPluginRewardedAdDelegate : NSObject <BDNRewardedAdDelegate>

@property (nonatomic, assign) DidLoad              rewardedDidLoadCallback;
@property (nonatomic, assign) DidFailToLoad        rewardedDidFailToLoadCallback;
@property (nonatomic, assign) WillPresent          rewardedWillPresentCallback;
@property (nonatomic, assign) DidFailToPresent     rewardedDidFailToPresentCallback;
@property (nonatomic, assign) DidClick             rewardedDidClickCallback;
@property (nonatomic, assign) DidHide              rewardedDidHideCallback;
@property (nonatomic, assign) DidExpire            rewardedDidExpireCallback;
@property (nonatomic, assign) DidPayRevenue        rewardedDidPayRevenueCallback;
@property (nonatomic, assign) DidReceiveReward     rewardedDidReceiveRewardCallback;

@end
