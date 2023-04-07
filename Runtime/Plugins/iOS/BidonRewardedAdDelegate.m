//
//  BidonRewardedAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonRewardedAdDelegate.h"
#import "BidonHelperMethods.h"

void* BDNUnityPluginCreateRewardedDelegate(DidFailToLoad didFailToLoadCallback,
                                           DidLoad didLoadCallback,
                                           DidFailToPresent didFailToPresentCallback,
                                           WillPresent willPresentCallback,
                                           DidHide didHideCallback,
                                           DidClick didClickCallback,
                                           DidPayRevenue didPayRevenueCallback,
                                           DidReceiveReward didReceiveRewardCallback) {
    BDNUnityPluginRewardedAdDelegate* delegate = [BDNUnityPluginRewardedAdDelegate new];
    delegate.rewardedDidFailToLoadCallback = didFailToLoadCallback;
    delegate.rewardedDidLoadCallback = didLoadCallback;
    delegate.rewardedDidFailToPresentCallback = didFailToPresentCallback;
    delegate.rewardedWillPresentCallback = willPresentCallback;
    delegate.rewardedDidHideCallback = didHideCallback;
    delegate.rewardedDidClickCallback = didClickCallback;
    delegate.rewardedDidPayRevenueCallback = didPayRevenueCallback;
    delegate.rewardedDidReceiveRewardCallback = didReceiveRewardCallback;
    return (__bridge_retained void*)delegate;
}

void BDNUnityPluginDestroyRewardedDelegate(void* delegatePtr) {
    if (!delegatePtr) return;

    BDNUnityPluginRewardedAdDelegate* delegate = (__bridge_transfer BDNUnityPluginRewardedAdDelegate *)delegatePtr;

    delegate.rewardedDidFailToLoadCallback = nil;
    delegate.rewardedDidLoadCallback = nil;
    delegate.rewardedDidFailToPresentCallback = nil;
    delegate.rewardedWillPresentCallback = nil;
    delegate.rewardedDidHideCallback = nil;
    delegate.rewardedDidClickCallback = nil;
    delegate.rewardedDidPayRevenueCallback = nil;
    delegate.rewardedDidReceiveRewardCallback = nil;
}

@implementation BDNUnityPluginRewardedAdDelegate

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didFailToLoadAd:(NSError * _Nonnull)error {
    if (!self.rewardedDidFailToLoadCallback) return;

    self.rewardedDidFailToLoadCallback((int)error.code);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didLoadAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidLoadCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.rewardedDidLoadCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didFailToPresentAd:(NSError * _Nonnull)error {
    if (!self.rewardedDidFailToPresentCallback) return;

    self.rewardedDidFailToPresentCallback((int)error.code);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd willPresentAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedWillPresentCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.rewardedWillPresentCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didDismissAd:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidHideCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.rewardedDidHideCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didRecordClick:(id<BDNAd> _Nonnull)ad {
    if (!self.rewardedDidClickCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.rewardedDidClickCallback(&unityAd);
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
