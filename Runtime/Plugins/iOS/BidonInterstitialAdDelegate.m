//
//  BidonInterstitialAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonInterstitialAdDelegate.h"
#import "BidonHelperMethods.h"

void* BDNUnityPluginCreateInterstitialDelegate(DidFailToLoad didFailToLoadCallback,
                                               DidLoad didLoadCallback,
                                               DidFailToPresent didFailToPresentCallback,
                                               WillPresent willPresentCallback,
                                               DidHide didHideCallback,
                                               DidClick didClickCallback,
                                               DidPayRevenue didPayRevenueCallback) {
    BDNUnityPluginInterstitialAdDelegate* delegate = [BDNUnityPluginInterstitialAdDelegate new];
    delegate.interstitialDidFailToLoadCallback = didFailToLoadCallback;
    delegate.interstitialDidLoadCallback = didLoadCallback;
    delegate.interstitialDidFailToPresentCallback = didFailToPresentCallback;
    delegate.interstitialWillPresentCallback = willPresentCallback;
    delegate.interstitialDidHideCallback = didHideCallback;
    delegate.interstitialDidClickCallback = didClickCallback;
    delegate.interstitialDidPayRevenueCallback = didPayRevenueCallback;
    return (__bridge_retained void*)delegate;
}

void BDNUnityPluginDestroyInterstitialDelegate(void* delegatePtr) {
    if (!delegatePtr) return;

    BDNUnityPluginInterstitialAdDelegate* delegate = (__bridge_transfer BDNUnityPluginInterstitialAdDelegate *)delegatePtr;

    delegate.interstitialDidFailToLoadCallback = nil;
    delegate.interstitialDidLoadCallback = nil;
    delegate.interstitialDidFailToPresentCallback = nil;
    delegate.interstitialWillPresentCallback = nil;
    delegate.interstitialDidHideCallback = nil;
    delegate.interstitialDidClickCallback = nil;
    delegate.interstitialDidPayRevenueCallback = nil;
}

@implementation BDNUnityPluginInterstitialAdDelegate

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didFailToLoadAd:(NSError * _Nonnull)error {
    if (!self.interstitialDidFailToLoadCallback) return;

    self.interstitialDidFailToLoadCallback((int)error.code);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didLoadAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidLoadCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.interstitialDidLoadCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didFailToPresentAd:(NSError * _Nonnull)error {
    if (!self.interstitialDidFailToPresentCallback) return;

    self.interstitialDidFailToPresentCallback((int)error.code);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd willPresentAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialWillPresentCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.interstitialWillPresentCallback(&unityAd);
}

- (void)fullscreenAd:(id<BDNFullscreenAd> _Nonnull)fullscreenAd didDismissAd:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidHideCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.interstitialDidHideCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didRecordClick:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidClickCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);

    self.interstitialDidClickCallback(&unityAd);
}

- (void)adObject:(id<BDNAdObject> _Nonnull)adObject didPay:(id<BDNAdRevenue> _Nonnull)revenue ad:(id<BDNAd> _Nonnull)ad {
    if (!self.interstitialDidPayRevenueCallback) return;

    BDNUnityPluginAd unityAd = GetBDNUnityPluginAd(ad);
    BDNUnityPluginAdRevenue unityAdRevenue = GetBDNUnityPluginAdRevenue(revenue);

    self.interstitialDidPayRevenueCallback(&unityAd, &unityAdRevenue);
}

@end
