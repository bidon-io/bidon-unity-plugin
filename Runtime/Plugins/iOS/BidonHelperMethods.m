//
//  BidonHelperMethods.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 07/04/2023.
//

#import <BidonHelperMethods.h>

BDNUnityPluginAdUnit BDNUnityPluginHelperGetAdUnit(id<BDNAdNetworkUnit>adUnit) {
    BDNUnityPluginAdUnit unityAdUnit;
    unityAdUnit.Uid = adUnit.uid ? [adUnit.uid UTF8String] : nil;
    unityAdUnit.DemandId = adUnit.demandId ? [adUnit.demandId UTF8String] : nil;
    unityAdUnit.Label = adUnit.label ? [adUnit.label UTF8String] : nil;
    unityAdUnit.PriceFloor = adUnit.pricefloor;
    unityAdUnit.BidType = (int)adUnit.bidType;
    // unityAdUnit.Extras = adUnit.extras ? [adUnit.extras UTF8String] : nil; // TODO: Convert to Json object and return as a string
    return unityAdUnit;
}

BDNUnityPluginAd BDNUnityPluginHelperGetAd(id<BDNAd>ad) {
    BDNUnityPluginAd unityAd;
    unityAd.AdUnit = BDNUnityPluginHelperGetAdUnit(ad.adUnit);
    unityAd.AuctionId = ad.auctionId ? [ad.auctionId UTF8String] : nil;
    unityAd.CurrencyCode = ad.currencyCode ? [ad.currencyCode UTF8String] : nil;
    unityAd.AdType = (int)ad.adType;
    unityAd.Ecpm = ad.price;
    unityAd.NetworkName = [ad.networkName UTF8String];
    return unityAd;
}

BDNUnityPluginAdUnitInfo BDNUnityPluginHelperGetAdUnitInfo(id<BDNAdUnitInfo>adUnitInfo) {
    BDNUnityPluginAdUnitInfo unityAdUnitInfo;
    unityAdUnitInfo.DemandId = [adUnitInfo.demandId UTF8String];
    unityAdUnitInfo.Label = [adUnitInfo.label UTF8String];
    unityAdUnitInfo.Price = [adUnitInfo.price doubleValue];
    unityAdUnitInfo.Uid = [adUnitInfo.uid UTF8String];
    unityAdUnitInfo.BidType = [adUnitInfo.bidType UTF8String];
    unityAdUnitInfo.FillStartTs = [adUnitInfo.fillStartTs longValue];
    unityAdUnitInfo.FillFinishTs = [adUnitInfo.fillFinishTs longValue];
    unityAdUnitInfo.Status = [adUnitInfo.status UTF8String];
    // unityAdUnitInfo.Ext = [adUnitInfo.ext UTF8String];
    return unityAdUnitInfo;
}

BDNUnityPluginAdUnitInfoArray BDNUnityPluginHelperGetAdUnitInfoArray(NSArray<id<BDNAdUnitInfo>>* adUnitInfoArray) {
    BDNUnityPluginAdUnitInfoArray unityAdUnitInfoArray;
    unityAdUnitInfoArray.values = malloc(sizeof(BDNUnityPluginAdUnitInfo) * adUnitInfoArray.count);
    for (int i = 0; i < adUnitInfoArray.count; i++) {
        unityAdUnitInfoArray.values[i] = BDNUnityPluginHelperGetAdUnitInfo(adUnitInfoArray[i]);
    }
    unityAdUnitInfoArray.length = (int)adUnitInfoArray.count;
    return unityAdUnitInfoArray;
}

BDNUnityPluginAuctionInfo BDNUnityPluginHelperGetAuctionInfo(id<BDNAuctionInfo>auctionInfo) {
    BDNUnityPluginAuctionInfo unityAuctionInfo;
    unityAuctionInfo.AuctionId = [auctionInfo.auctionId UTF8String];
    unityAuctionInfo.AuctionConfigurationId = [auctionInfo.auctionConfigurationId UTF8String];
    unityAuctionInfo.AuctionConfigurationUid = [auctionInfo.auctionConfigurationUid UTF8String];
    unityAuctionInfo.AuctionPriceFloor = [auctionInfo.auctionPricefloor doubleValue];
    unityAuctionInfo.NoBids = BDNUnityPluginHelperGetAdUnitInfoArray(auctionInfo.noBids);
    unityAuctionInfo.AdUnits = BDNUnityPluginHelperGetAdUnitInfoArray(auctionInfo.adUnits);
    unityAuctionInfo.Timeout = [auctionInfo.timeout longValue];
    unityAuctionInfo.Description = [auctionInfo.description UTF8String];
    return unityAuctionInfo;
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
