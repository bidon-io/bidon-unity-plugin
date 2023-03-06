//
//  BidonRewardedAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <BidOn/BidOn-Swift.h>
#import <BidonUnityPluginStructs.h>

typedef void (*DidStartAuction)();
typedef void (*DidCompleteAuction)(BDNUnityPluginAd* winner);
typedef void (*DidStartAuctionRound)(const char* roundId, double priceFloor);
typedef void (*DidCompleteAuctionRound)(BDNUnityPluginAuctionRound* round);
typedef void (*DidReceiveBid)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToPresent)(BDNUnityPluginImpression* impression, int error);
typedef void (*WillPresent)(BDNUnityPluginImpression* impression);
typedef void (*DidHide)(BDNUnityPluginImpression* impression);
typedef void (*DidClick)(BDNUnityPluginImpression* impression);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);
typedef void (*DidReceiveReward)(BDNUnityPluginReward* reward, BDNUnityPluginImpression* impression);

@interface BDNUnityPluginRewardedAdDelegate : NSObject <BDNRewardedAdDelegate>

@property (assign) DidStartAuction           rewardedDidStartAuctionCallback;
@property (assign) DidCompleteAuction        rewardedDidCompleteAuctionCallback;
@property (assign) DidStartAuctionRound      rewardedDidStartAuctionRoundCallback;
@property (assign) DidCompleteAuctionRound   rewardedDidCompleteAuctionRoundCallback;
@property (assign) DidReceiveBid             rewardedDidReceiveBidCallback;
@property (assign) DidFailToLoad             rewardedDidFailToLoadCallback;
@property (assign) DidLoad                   rewardedDidLoadCallback;
@property (assign) DidFailToPresent          rewardedDidFailToPresentCallback;
@property (assign) WillPresent               rewardedWillPresentCallback;
@property (assign) DidHide                   rewardedDidHideCallback;
@property (assign) DidClick                  rewardedDidClickCallback;
@property (assign) DidPayRevenue             rewardedDidPayRevenueCallback;
@property (assign) DidReceiveReward          rewardedDidReceiveRewardCallback;

@end
