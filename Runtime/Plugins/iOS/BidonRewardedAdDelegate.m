//
//  BidonRewardedAdDelegate.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import "BidonRewardedAdDelegate.h"

void* BDNUnityPluginCreateRewardedDelegate(DidStartAuction didStartAuctionCallback,
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
                                           DidPayRevenue didPayRevenueCallback,
                                           DidReceiveReward didReceiveRewardCallback) {
    BDNUnityPluginRewardedAdDelegate* delegate = [BDNUnityPluginRewardedAdDelegate new];
    delegate.rewardedDidStartAuctionCallback = didStartAuctionCallback;
    delegate.rewardedDidCompleteAuctionCallback = didCompleteAuctionCallback;
    delegate.rewardedDidStartAuctionRoundCallback = didStartAuctionRoundCallback;
    delegate.rewardedDidCompleteAuctionRoundCallback = didCompleteAuctionRoundCallback;
    delegate.rewardedDidReceiveBidCallback = didReceiveBidCallback;
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

    delegate.rewardedDidStartAuctionCallback = nil;
    delegate.rewardedDidCompleteAuctionCallback = nil;
    delegate.rewardedDidStartAuctionRoundCallback = nil;
    delegate.rewardedDidCompleteAuctionRoundCallback = nil;
    delegate.rewardedDidReceiveBidCallback = nil;
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

- (void)adObjectDidStartAuction:(id<BDNAdObject>)adObject {
    if (!self.rewardedDidStartAuctionCallback) return;

    self.rewardedDidStartAuctionCallback();
}

- (void)adObject:(id<BDNAdObject>)adObject didCompleteAuction:(id<BDNAd>)winner {
    if (!self.rewardedDidCompleteAuctionCallback) return;

    BDNUnityPluginAd unityAd;
    if (winner) {
        unityAd.Id = [winner.id UTF8String];
        unityAd.Ecpm = winner.eCPM;
        unityAd.AdUnitId = winner.adUnitId ? [winner.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [winner.networkName UTF8String];
        unityAd.Dsp = winner.dsp ? [winner.dsp UTF8String] : nil;
    }

    self.rewardedDidCompleteAuctionCallback(winner ? &unityAd : nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didStartAuctionRound:(NSString *)auctionRound pricefloor:(double)pricefloor {
    if (!self.rewardedDidStartAuctionRoundCallback) return;

    self.rewardedDidStartAuctionRoundCallback([auctionRound UTF8String], pricefloor);
}

- (void)adObject:(id<BDNAdObject>)adObject didCompleteAuctionRound:(NSString *)auctionRound {
    if (!self.rewardedDidCompleteAuctionRoundCallback) return;

    self.rewardedDidCompleteAuctionRoundCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didReceiveBid:(id<BDNAd>)ad {
    if (!self.rewardedDidReceiveBidCallback) return;

    BDNUnityPluginAd unityAd;
    if (ad) {
        unityAd.Id = [ad.id UTF8String];
        unityAd.Ecpm = ad.eCPM;
        unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [ad.networkName UTF8String];
        unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    }

    self.rewardedDidReceiveBidCallback(ad ? &unityAd : nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didFailToLoadAd:(NSError *)error {
    if (!self.rewardedDidFailToLoadCallback) return;

    self.rewardedDidFailToLoadCallback(0);
}

- (void)adObject:(id<BDNAdObject>)adObject didLoadAd:(id<BDNAd>)ad {
    if (!self.rewardedDidLoadCallback) return;

    BDNUnityPluginAd unityAd;
    if (ad) {
        unityAd.Id = [ad.id UTF8String];
        unityAd.Ecpm = ad.eCPM;
        unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
        unityAd.NetworkName = [ad.networkName UTF8String];
        unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    }

    self.rewardedDidLoadCallback(ad ? &unityAd : nil);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd didFailToPresentAd:(NSError *)error {
    if (!self.rewardedDidFailToPresentCallback) return;

    self.rewardedDidFailToPresentCallback(nil, 0);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd willPresentAd:(id<BDNAd>)ad {
    if (!self.rewardedWillPresentCallback) return;

    self.rewardedWillPresentCallback(nil);
}

- (void)fullscreenAd:(id<BDNFullscreenAd>)fullscreenAd didDismissAd:(id<BDNAd>)ad {
    if (!self.rewardedDidHideCallback) return;

    self.rewardedDidHideCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didRecordClick:(id<BDNAd>)ad {
    if (!self.rewardedDidClickCallback) return;

    self.rewardedDidClickCallback(nil);
}

- (void)adObject:(id<BDNAdObject>)adObject didPay:(id<BDNAdRevenue>)revenue ad:(id<BDNAd>)ad {
    if (!self.rewardedDidPayRevenueCallback) return;

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

    self.rewardedDidPayRevenueCallback(ad ? &unityAd : nil, revenue ? &unityAdRevenue : nil);
}

- (void)rewardedAd:(id<BDNRewardedAd>)rewardedAd didRewardUser:(id<BDNReward>)reward {
    if (!self.rewardedDidReceiveRewardCallback) return;

    BDNUnityPluginReward unityReward;
    if (reward) {
        unityReward.label = [reward.label UTF8String];
        unityReward.amount = reward.amount;
    }

    self.rewardedDidReceiveRewardCallback(reward ? &unityReward : nil, nil);
}
@end
