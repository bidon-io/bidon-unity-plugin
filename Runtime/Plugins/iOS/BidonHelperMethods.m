//
//  BidonHelperMethods.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 07/04/2023.
//

#import <BidonHelperMethods.h>

BDNUnityPluginAd BDNUnityPluginHelperGetAd(id<BDNAd>ad) {
    BDNUnityPluginAd unityAd;
    unityAd.AdUnitId = ad.adUnitId ? [ad.adUnitId UTF8String] : nil;
    unityAd.AuctionId = ad.auctionId ? [ad.auctionId UTF8String] : nil;
    unityAd.CurrencyCode = ad.currencyCode ? [ad.currencyCode UTF8String] : nil;
    unityAd.AdType = (int)ad.adType;
    unityAd.BidType = (int)ad.bidType;
    unityAd.Dsp = ad.dsp ? [ad.dsp UTF8String] : nil;
    unityAd.Ecpm = ad.eCPM;
    unityAd.NetworkName = [ad.networkName UTF8String];
    unityAd.RoundId = ad.roundId ? [ad.roundId UTF8String] : nil;
    return unityAd;
}

BDNUnityPluginAdRevenue BDNUnityPluginHelperGetAdRevenue(id<BDNAdRevenue>revenue) {
    BDNUnityPluginAdRevenue unityAdRevenue;
    unityAdRevenue.Revenue = revenue.revenue;
    unityAdRevenue.RevenuePrecision = (int)revenue.precision;
    unityAdRevenue.Currency = [revenue.currency UTF8String];
    return unityAdRevenue;
}

BDNUnityPluginReward BDNUnityPluginHelperGetReward(id<BDNReward>reward) {
    BDNUnityPluginReward unityReward;
    unityReward.label = [reward.label UTF8String];
    unityReward.amount = reward.amount;
    return unityReward;
}
