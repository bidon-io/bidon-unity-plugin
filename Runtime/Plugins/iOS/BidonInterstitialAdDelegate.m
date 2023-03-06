//
//  BidonInterstitialAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonInterstitialAdDelegate.h"

void* BDNUnityPluginCreateInterstitialDelegate(DidStartAuction didStartAuctionCallback,
                                               DidCompleteAuction didCompleteAuctionCallback,
                                               DidStartAuctionRound didStartAuctionRoundCallback,
                                               DidCompleteAuctionRound didCompleteAuctionRoundCallback,
                                               DidReceiveBid didReceiveBidCallback,
                                               DidFailToLoad didFailToLoadCallback,
                                               DidLoad didLoadCallback,
                                               DidFailToPresent didFailToPresentCallback,
                                               WillPresent willPresentCallback,
                                               DidHide didHideCallback,
                                               DidClick didClickCallback,
                                               DidPayRevenue didPayRevenueCallback) {
    BDNUnityPluginInterstitialAdDelegate* delegate = [BDNUnityPluginInterstitialAdDelegate new];
    delegate.interstitialDidStartAuctionCallback = didStartAuctionCallback;
    delegate.interstitialDidCompleteAuctionCallback = didCompleteAuctionCallback;
    delegate.interstitialDidStartAuctionRoundCallback = didStartAuctionRoundCallback;
    delegate.interstitialDidCompleteAuctionRoundCallback = didCompleteAuctionRoundCallback;
    delegate.interstitialDidReceiveBidCallback = didReceiveBidCallback;
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

    delegate.interstitialDidStartAuctionCallback = nil;
    delegate.interstitialDidCompleteAuctionCallback = nil;
    delegate.interstitialDidStartAuctionRoundCallback = nil;
    delegate.interstitialDidCompleteAuctionRoundCallback = nil;
    delegate.interstitialDidReceiveBidCallback = nil;
    delegate.interstitialDidFailToLoadCallback = nil;
    delegate.interstitialDidLoadCallback = nil;
    delegate.interstitialDidFailToPresentCallback = nil;
    delegate.interstitialWillPresentCallback = nil;
    delegate.interstitialDidHideCallback = nil;
    delegate.interstitialDidClickCallback = nil;
    delegate.interstitialDidPayRevenueCallback = nil;
}

@implementation BDNUnityPluginInterstitialAdDelegate

- (void)adObjectDidStartAuction:(id<BDNAdObject>)adObject {
    if (!self.interstitialDidStartAuctionCallback) return;

    self.interstitialDidStartAuctionCallback();
}

- (void)adObject:(id<BDNAdObject>)adObject didCompleteAuction:(id<BDNAd>)winner {
    if (!self.interstitialDidCompleteAuctionCallback) return;

    BDNUnityPluginAd unityAd;
    if (winner) {
        unityAd.Id = [winner.id UTF8String];
        unityAd.Ecpm = winner.eCPM;
        unityAd.AdUnitId = winner.adUnitId ? [winner.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [winner.networkName UTF8String];
        unityAd.Dsp = winner.dsp ? [winner.dsp UTF8String] : nil;
    }

    self.interstitialDidCompleteAuctionCallback(winner ? &unityAd : nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didStartAuctionRound:(NSString *)auctionRound pricefloor:(double)pricefloor {
    if (!self.interstitialDidStartAuctionRoundCallback) return;

    self.interstitialDidStartAuctionRoundCallback([auctionRound UTF8String], pricefloor);
}

- (void)adObject:(id<BDNAdObject>)adObject didCompleteAuctionRound:(NSString *)auctionRound {
    if (!self.interstitialDidCompleteAuctionRoundCallback) return;

    self.interstitialDidCompleteAuctionRoundCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didReceiveBid:(id<BDNAd>)ad {
    if (!self.interstitialDidReceiveBidCallback) return;

    BDNUnityPluginAd unityAd;
    if (ad) {
        unityAd.Id = [ad.id UTF8String];
        unityAd.Ecpm = ad.eCPM;
        unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [ad.networkName UTF8String];
        unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    }

    self.interstitialDidReceiveBidCallback(ad ? &unityAd : nil);
}

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
