//
//  BidonRewardedAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonHelperMethods.h"
#import "BidonRewardedAdDelegate.h"

CFBDNUnityPluginRewardedAdDelegateRef BDNUnityPluginRewardedAdDelegateCreate(DidLoad didLoadCallback,
                                                                             DidFailToLoad didFailToLoadCallback,
                                                                             WillPresent willPresentCallback,
                                                                             DidFailToPresent didFailToPresentCallback,
                                                                             DidClick didClickCallback,
                                                                             DidHide didHideCallback,
                                                                             DidExpire didExpireCallback,
                                                                             DidPayRevenue didPayRevenueCallback,
                                                                             DidReceiveReward didReceiveRewardCallback) {
    BDNUnityPluginRewardedAdDelegate* delegate = [BDNUnityPluginRewardedAdDelegate new];
    delegate.rewardedDidLoadCallback = didLoadCallback;
    delegate.rewardedDidFailToLoadCallback = didFailToLoadCallback;
    delegate.rewardedWillPresentCallback = willPresentCallback;
    delegate.rewardedDidFailToPresentCallback = didFailToPresentCallback;
    delegate.rewardedDidClickCallback = didClickCallback;
    delegate.rewardedDidHideCallback = didHideCallback;
    delegate.rewardedDidExpireCallback = didExpireCallback;
    delegate.rewardedDidPayRevenueCallback = didPayRevenueCallback;
    delegate.rewardedDidReceiveRewardCallback = didReceiveRewardCallback;

    return (__bridge_retained CFBDNUnityPluginRewardedAdDelegateRef)delegate;
}

void BDNUnityPluginRewardedAdDelegateDestroy(CFBDNUnityPluginRewardedAdDelegateRef delegatePtr) {
    if (!delegatePtr) return;

    BDNUnityPluginRewardedAdDelegate* delegate = (__bridge_transfer BDNUnityPluginRewardedAdDelegate*)delegatePtr;

    delegate.rewardedDidLoadCallback = NULL;
    delegate.rewardedDidFailToLoadCallback = NULL;
    delegate.rewardedWillPresentCallback = NULL;
    delegate.rewardedDidFailToPresentCallback = NULL;
    delegate.rewardedDidClickCallback = NULL;
    delegate.rewardedDidHideCallback = NULL;
    delegate.rewardedDidExpireCallback = NULL;
    delegate.rewardedDidPayRevenueCallback = NULL;
    delegate.rewardedDidReceiveRewardCallback = NULL;
}

@implementation BDNUnityPluginRewardedAdDelegate

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didLoadAd:(id<BDNAd> _Nonnull)ad auctionInfo:(id<BDNAuctionInfo> _Nonnull)auctionInfo {
    if (!self.rewardedDidLoadCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    BDNUnityPluginAuctionInfo* unityAuctionInfo = BDNUnityPluginHelperGetAuctionInfo(auctionInfo);
    self.rewardedDidLoadCallback(unityAd, unityAuctionInfo);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didFailToLoadAd:(NSError * _Nonnull)error auctionInfo:(id<BDNAuctionInfo> _Nonnull)auctionInfo {
    if (!self.rewardedDidFailToLoadCallback) return;

    BDNUnityPluginAuctionInfo* unityAuctionInfo = BDNUnityPluginHelperGetAuctionInfo(auctionInfo);
    self.rewardedDidFailToLoadCallback(unityAuctionInfo, (int)error.code);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd willPresentAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedWillPresentCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    self.rewardedWillPresentCallback(unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didFailToPresentAd:(NSError * _Nonnull)error {
    if (!self.rewardedDidFailToPresentCallback) return;

    self.rewardedDidFailToPresentCallback((int)error.code);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didRecordClick:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidClickCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    self.rewardedDidClickCallback(unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didDismissAd:(id<BDNAd> _Nonnull)ad {
    extern bool _didResignActive;
    if(_didResignActive) return;

    if (!self.rewardedDidHideCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    self.rewardedDidHideCallback(unityAd);
}

- (void)adObject:(id<BDNAdObject>)adObject didExpireAd:(id<BDNAd>)ad {
    if (!self.rewardedDidExpireCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    self.rewardedDidExpireCallback(unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didPay:(id<BDNAdRevenue> _Nonnull)revenue ad:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidPayRevenueCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    BDNUnityPluginAdRevenue* unityAdRevenue = BDNUnityPluginHelperGetAdRevenue(revenue);
    self.rewardedDidPayRevenueCallback(unityAd, unityAdRevenue);
}

- (void)rewardedAd:(id<BDNRewardedAd> _Nonnull)rewardedAd didRewardUser:(id<BDNReward> _Nonnull)reward ad:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidReceiveRewardCallback) return;

    BDNUnityPluginAd* unityAd = BDNUnityPluginHelperGetAd(ad);
    BDNUnityPluginReward* unityReward = BDNUnityPluginHelperGetReward(reward);
    self.rewardedDidReceiveRewardCallback(unityAd, unityReward);
}

@end
