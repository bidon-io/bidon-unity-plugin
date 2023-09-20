//
//  BidonRewardedAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonHelperMethods.h>

@interface BDNUnityPluginRewardedAdDelegate : NSObject <BDNRewardedAdDelegate>

@property (assign) DidLoad              rewardedDidLoadCallback;
@property (assign) DidFailToLoad        rewardedDidFailToLoadCallback;
@property (assign) WillPresent          rewardedWillPresentCallback;
@property (assign) DidFailToPresent     rewardedDidFailToPresentCallback;
@property (assign) DidClick             rewardedDidClickCallback;
@property (assign) DidHide              rewardedDidHideCallback;
@property (assign) DidExpire            rewardedDidExpireCallback;
@property (assign) DidPayRevenue        rewardedDidPayRevenueCallback;
@property (assign) DidReceiveReward     rewardedDidReceiveRewardCallback;

@end
