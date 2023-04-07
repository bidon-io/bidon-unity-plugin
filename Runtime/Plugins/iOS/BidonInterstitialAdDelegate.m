//
//  BidonInterstitialAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonInterstitialAdDelegate.h"

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

- (void)adObject:(id<BDNAdObject>)adObject didFailToLoadAd:(NSError *)error {
    if (!self.interstitialDidFailToLoadCallback) return;

    self.interstitialDidFailToLoadCallback(0);
}

- (void)adObject:(id<BDNAdObject>)adObject didLoadAd:(id<BDNAd>)ad {
    if (!self.interstitialDidLoadCallback) return;

    BDNUnityPluginAd unityAd;
    if (ad) {
        unityAd.Id = [ad.id UTF8String];
        unityAd.Ecpm = ad.eCPM;
        unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [ad.networkName UTF8String];
        unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    }

    self.interstitialDidLoadCallback(ad ? &unityAd : nil);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd didFailToPresentAd:(NSError *)error {
    if (!self.interstitialDidFailToPresentCallback) return;

    self.interstitialDidFailToPresentCallback(nil, 0);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd willPresentAd:(id<BDNAd>)ad {
    if (!self.interstitialWillPresentCallback) return;

    self.interstitialWillPresentCallback(nil);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd didDismissAd:(id<BDNAd>)ad {
    if (!self.interstitialDidHideCallback) return;

    self.interstitialDidHideCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didRecordClick:(id<BDNAd>)ad {
    if (!self.interstitialDidClickCallback) return;

    self.interstitialDidClickCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didPay:(id<BDNAdRevenue>)revenue ad:(id<BDNAd>)ad {
    if (!self.interstitialDidPayRevenueCallback) return;

    BDNUnityPluginAd unityAd;
    if (ad) {
        unityAd.Id = [ad.id UTF8String];
        unityAd.Ecpm = ad.eCPM;
        unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [ad.networkName UTF8String];
        unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    }

    BDNUnityPluginAdRevenue unityAdRevenue;
    if (revenue) {
        unityAdRevenue.Revenue = revenue.revenue;
        unityAdRevenue.RevenuePrecision = (int)revenue.precision;
        unityAdRevenue.Currency = [revenue.currency UTF8String];
    }

    self.interstitialDidPayRevenueCallback(ad ? &unityAd : nil, revenue ? &unityAdRevenue : nil);
}

@end
