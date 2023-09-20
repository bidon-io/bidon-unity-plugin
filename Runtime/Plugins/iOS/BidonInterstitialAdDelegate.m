//
//  BidonInterstitialAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonInterstitialAdDelegate.h"

CFBDNUnityPluginInterstitialAdDelegateRef BDNUnityPluginInterstitialAdDelegateCreate(DidLoad didLoadCallback,
                                                                                     DidFailToLoad didFailToLoadCallback,
                                                                                     WillPresent willPresentCallback,
                                                                                     DidFailToPresent didFailToPresentCallback,
                                                                                     DidClick didClickCallback,
                                                                                     DidHide didHideCallback,
                                                                                     DidExpire didExpireCallback,
                                                                                     DidPayRevenue didPayRevenueCallback) {
    BDNUnityPluginInterstitialAdDelegate* delegate = [BDNUnityPluginInterstitialAdDelegate new];
    delegate.interstitialDidLoadCallback = didLoadCallback;
    delegate.interstitialDidFailToLoadCallback = didFailToLoadCallback;
    delegate.interstitialWillPresentCallback = willPresentCallback;
    delegate.interstitialDidFailToPresentCallback = didFailToPresentCallback;
    delegate.interstitialDidClickCallback = didClickCallback;
    delegate.interstitialDidHideCallback = didHideCallback;
    delegate.interstitialDidExpireCallback = didExpireCallback;
    delegate.interstitialDidPayRevenueCallback = didPayRevenueCallback;

    return (__bridge_retained CFBDNUnityPluginInterstitialAdDelegateRef)delegate;
}

void BDNUnityPluginInterstitialAdDelegateDestroy(CFBDNUnityPluginInterstitialAdDelegateRef delegatePtr) {
    if (!delegatePtr) return;

    BDNUnityPluginInterstitialAdDelegate* delegate = (__bridge_transfer BDNUnityPluginInterstitialAdDelegate*)delegatePtr;

    delegate.interstitialDidLoadCallback = nil;
    delegate.interstitialDidFailToLoadCallback = nil;
    delegate.interstitialWillPresentCallback = nil;
    delegate.interstitialDidFailToPresentCallback = nil;
    delegate.interstitialDidClickCallback = nil;
    delegate.interstitialDidHideCallback = nil;
    delegate.interstitialDidExpireCallback = nil;
    delegate.interstitialDidPayRevenueCallback = nil;
}

@implementation BDNUnityPluginInterstitialAdDelegate

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didLoadAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidLoadCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    self.interstitialDidLoadCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didFailToLoadAd:(NSError * _Nonnull)error {
    if (!self.interstitialDidFailToLoadCallback) return;

    self.interstitialDidFailToLoadCallback((int)error.code);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd willPresentAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialWillPresentCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    self.interstitialWillPresentCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didFailToPresentAd:(NSError * _Nonnull)error {
    if (!self.interstitialDidFailToPresentCallback) return;

    self.interstitialDidFailToPresentCallback((int)error.code);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didRecordClick:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidClickCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    self.interstitialDidClickCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didDismissAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidHideCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    self.interstitialDidHideCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject>)adObject didExpireAd:(id<BDNAd>)ad {
    if (!self.interstitialDidExpireCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    self.interstitialDidExpireCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didPay:(id<BDNAdRevenue> _Nonnull)revenue ad:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidPayRevenueCallback) return;

    BDNUnityPluginAd unityAd = BDNUnityPluginHelperGetAd(ad);
    BDNUnityPluginAdRevenue unityAdRevenue = BDNUnityPluginHelperGetAdRevenue(revenue);
    self.interstitialDidPayRevenueCallback(&unityAd, &unityAdRevenue);
}

@end
