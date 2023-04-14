//
//  BidonRewardedAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonRewardedAdDelegate.h"
#import "BidonHelperMethods.h"

void* BDNUnityPluginCreateRewardedDelegate(DidLoad didLoadCallback,
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

    return (__bridge_retained void*)delegate;
}

void BDNUnityPluginDestroyRewardedDelegate(void* delegatePtr) {
    if (!delegatePtr) return;

    BDNUnityPluginRewardedAdDelegate* delegate = (__bridge_transfer BDNUnityPluginRewardedAdDelegate *)delegatePtr;

    delegate.rewardedDidLoadCallback = nil;
    delegate.rewardedDidFailToLoadCallback = nil;
    delegate.rewardedWillPresentCallback = nil;
    delegate.rewardedDidFailToPresentCallback = nil;
    delegate.rewardedDidClickCallback = nil;
    delegate.rewardedDidHideCallback = nil;
    delegate.rewardedDidExpireCallback = nil;
    delegate.rewardedDidPayRevenueCallback = nil;
    delegate.rewardedDidReceiveRewardCallback = nil;
}

@implementation BDNUnityPluginRewardedAdDelegate

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didLoadAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidLoadCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    self.rewardedDidLoadCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didFailToLoadAd:(NSError * _Nonnull)error {
    if (!self.rewardedDidFailToLoadCallback) return;

    self.rewardedDidFailToLoadCallback((int)error.code);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd willPresentAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedWillPresentCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    self.rewardedWillPresentCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didFailToPresentAd:(NSError * _Nonnull)error {
    if (!self.rewardedDidFailToPresentCallback) return;

    self.rewardedDidFailToPresentCallback((int)error.code);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didRecordClick:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidClickCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    self.rewardedDidClickCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didDismissAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidHideCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    self.rewardedDidHideCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject>)adObject didExpireAd:(id<BDNAd>)ad {
    if (!self.rewardedDidExpireCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    self.rewardedDidExpireCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didPay:(id<BDNAdRevenue> _Nonnull)revenue ad:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidPayRevenueCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    BDNUnityPluginAdRevenue unityAdRevenue = GetBDNUnityPluginAdRevenue(revenue);
    self.rewardedDidPayRevenueCallback(&unityAd, &unityAdRevenue);
}

- (void)rewardedAd:(id<BDNRewardedAd> _Nonnull)rewardedAd didRewardUser:(id<BDNReward> _Nonnull)reward ad:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidReceiveRewardCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    BDNUnityPluginReward unityReward = GetBDNUnityPluginReward(reward);
    self.rewardedDidReceiveRewardCallback(&unityAd, &unityReward);
}

@end
