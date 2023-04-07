//
//  BidonRewardedAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginStructs.h>

typedef void (*DidFailToLoad)(int error);
typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToPresent)(int error);
typedef void (*WillPresent)(BDNUnityPluginAd* ad);
typedef void (*DidHide)(BDNUnityPluginAd* ad);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);
typedef void (*DidReceiveReward)(BDNUnityPluginAd* ad, BDNUnityPluginReward* reward);

@interface BDNUnityPluginRewardedAdDelegate : NSObject <BDNRewardedAdDelegate>

@property (assign) DidFailToLoad        rewardedDidFailToLoadCallback;
@property (assign) DidLoad              rewardedDidLoadCallback;
@property (assign) DidFailToPresent     rewardedDidFailToPresentCallback;
@property (assign) WillPresent          rewardedWillPresentCallback;
@property (assign) DidHide              rewardedDidHideCallback;
@property (assign) DidClick             rewardedDidClickCallback;
@property (assign) DidPayRevenue        rewardedDidPayRevenueCallback;
@property (assign) DidReceiveReward     rewardedDidReceiveRewardCallback;

@end
