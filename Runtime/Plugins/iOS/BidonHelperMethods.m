//
//  BidonHelperMethods.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 07/04/2023.
//

#import <BidonHelperMethods.h>
#import <BidonUtilities.h>

#pragma mark - BDNUnityPluginAd struct helper methods

BDNUnityPluginAdUnit* BDNUnityPluginHelperGetAdUnit(id<BDNAdNetworkUnit>adUnit) {
    if (!adUnit) return NULL;

    BDNUnityPluginAdUnit* unityAdUnit = (BDNUnityPluginAdUnit*)malloc(sizeof(BDNUnityPluginAdUnit));
    if (!unityAdUnit) return NULL;

    unityAdUnit->Uid = CreateCString(adUnit.uid);
    unityAdUnit->DemandId = CreateCString(adUnit.demandId);
    unityAdUnit->Label = CreateCString(adUnit.label);
    unityAdUnit->PriceFloor = adUnit.pricefloor;
    unityAdUnit->BidType = (int)adUnit.bidType;
    unityAdUnit->Extras = CreateCString(adUnit.extrasJsonString);

    return unityAdUnit;
}

BDNUnityPluginAd* BDNUnityPluginHelperGetAd(id<BDNAd>ad) {
    if (!ad) return NULL;

    BDNUnityPluginAd* unityAd = (BDNUnityPluginAd*)malloc(sizeof(BDNUnityPluginAd));
    if (!unityAd) return NULL;

    unityAd->AdUnit = BDNUnityPluginHelperGetAdUnit(ad.adUnit);
    unityAd->AuctionId = CreateCString(ad.auctionId);
    unityAd->CurrencyCode = CreateCString(ad.currencyCode);
    unityAd->AdType = (int)ad.adType;
    unityAd->Dsp = CreateCString(ad.dsp);
    unityAd->Price = ad.price;
    unityAd->NetworkName = CreateCString(ad.networkName);

    return unityAd;
}

void BDNUnityPluginHelperFreeAd(BDNUnityPluginAd* ad) {
    if (!ad) return;

    free((void*)ad->AuctionId);
    free((void*)ad->CurrencyCode);
    free((void*)ad->Dsp);
    free((void*)ad->NetworkName);

    if (ad->AdUnit) {
        free((void*)ad->AdUnit->Uid);
        free((void*)ad->AdUnit->DemandId);
        free((void*)ad->AdUnit->Label);
        free((void*)ad->AdUnit->Extras);

        free(ad->AdUnit);
    }

    free(ad);
}

#pragma mark - BDNUnityPluginAuctionInfo struct helper methods

BDNUnityPluginAdUnitInfo* BDNUnityPluginHelperGetAdUnitInfo(id<BDNAdUnitInfo>adUnitInfo) {
    if (!adUnitInfo) return NULL;

    BDNUnityPluginAdUnitInfo* unityAdUnitInfo = (BDNUnityPluginAdUnitInfo*)malloc(sizeof(BDNUnityPluginAdUnitInfo));
    if (!unityAdUnitInfo) return NULL;

    unityAdUnitInfo->DemandId = CreateCString(adUnitInfo.demandId);
    unityAdUnitInfo->Label = CreateCString(adUnitInfo.label);
    unityAdUnitInfo->Price = adUnitInfo.price ? CreateCString([adUnitInfo.price stringValue]) : NULL;
    unityAdUnitInfo->Uid = CreateCString(adUnitInfo.uid);
    unityAdUnitInfo->BidType = CreateCString(adUnitInfo.bidType);
    unityAdUnitInfo->FillStartTs = adUnitInfo.fillStartTs ? CreateCString([adUnitInfo.fillStartTs stringValue]) : NULL;
    unityAdUnitInfo->FillFinishTs = adUnitInfo.fillFinishTs ? CreateCString([adUnitInfo.fillFinishTs stringValue]) : NULL;
    unityAdUnitInfo->Status = CreateCString(adUnitInfo.status);
    unityAdUnitInfo->Ext = CreateCString(adUnitInfo.extrasJsonString);

    return unityAdUnitInfo;
}

BDNUnityPluginAdUnitInfoArray* BDNUnityPluginHelperGetAdUnitInfoArray(NSArray<id<BDNAdUnitInfo>>* adUnitInfoArray) {
    if (!adUnitInfoArray) return NULL;

    BDNUnityPluginAdUnitInfoArray* unityAdUnitInfoArray = (BDNUnityPluginAdUnitInfoArray*)malloc(sizeof(BDNUnityPluginAdUnitInfoArray));
    if (!unityAdUnitInfoArray) return NULL;

    unityAdUnitInfoArray->Values = malloc(sizeof(BDNUnityPluginAdUnitInfo*) * adUnitInfoArray.count);
    if (!unityAdUnitInfoArray->Values) {
        free(unityAdUnitInfoArray);
        return NULL;
    }

    for (int i = 0; i < adUnitInfoArray.count; i++) {
        unityAdUnitInfoArray->Values[i] = BDNUnityPluginHelperGetAdUnitInfo(adUnitInfoArray[i]);
    }
    unityAdUnitInfoArray->Length = (int)adUnitInfoArray.count;

    return unityAdUnitInfoArray;
}

BDNUnityPluginAuctionInfo* BDNUnityPluginHelperGetAuctionInfo(id<BDNAuctionInfo>auctionInfo) {
    if (!auctionInfo) return NULL;

    BDNUnityPluginAuctionInfo* unityAuctionInfo = (BDNUnityPluginAuctionInfo*)malloc(sizeof(BDNUnityPluginAuctionInfo));
    if (!unityAuctionInfo) return NULL;

    unityAuctionInfo->AuctionId = CreateCString(auctionInfo.auctionId);
    unityAuctionInfo->AuctionConfigurationId = auctionInfo.auctionConfigurationId ? CreateCString([auctionInfo.auctionConfigurationId stringValue]) : NULL;
    unityAuctionInfo->AuctionConfigurationUid = CreateCString(auctionInfo.auctionConfigurationUid);
    unityAuctionInfo->AuctionTimeout = auctionInfo.timeout ? [auctionInfo.timeout longValue] : 0;
    unityAuctionInfo->AuctionPriceFloor = auctionInfo.auctionPricefloor ? [auctionInfo.auctionPricefloor doubleValue] : 0;
    unityAuctionInfo->NoBids = BDNUnityPluginHelperGetAdUnitInfoArray(auctionInfo.noBids);
    unityAuctionInfo->AdUnits = BDNUnityPluginHelperGetAdUnitInfoArray(auctionInfo.adUnits);

    return unityAuctionInfo;
}

void BDNUnityPluginHelperFreeAdUnitInfo(BDNUnityPluginAdUnitInfo* adUnitInfo) {
    if (!adUnitInfo) return;

    free((void*)adUnitInfo->DemandId);
    free((void*)adUnitInfo->Label);
    free((void*)adUnitInfo->Price);
    free((void*)adUnitInfo->Uid);
    free((void*)adUnitInfo->BidType);
    free((void*)adUnitInfo->FillStartTs);
    free((void*)adUnitInfo->FillFinishTs);
    free((void*)adUnitInfo->Status);
    free((void*)adUnitInfo->Ext);
}

void BDNUnityPluginHelperFreeAuctionInfo(BDNUnityPluginAuctionInfo* auctionInfo) {
    if (!auctionInfo) return;

    free((void*)auctionInfo->AuctionId);
    free((void*)auctionInfo->AuctionConfigurationId);
    free((void*)auctionInfo->AuctionConfigurationUid);

    if (auctionInfo->NoBids) {
        for (int i = 0; i < auctionInfo->NoBids->Length; i++) {
            BDNUnityPluginHelperFreeAdUnitInfo(auctionInfo->NoBids->Values[i]);
        }

        free(auctionInfo->NoBids->Values);
        free(auctionInfo->NoBids);
    }

    if (auctionInfo->AdUnits) {
        for (int i = 0; i < auctionInfo->AdUnits->Length; i++) {
            BDNUnityPluginHelperFreeAdUnitInfo(auctionInfo->AdUnits->Values[i]);
        }

        free(auctionInfo->AdUnits->Values);
        free(auctionInfo->AdUnits);
    }

    free(auctionInfo);
}

#pragma mark - BDNUnityPluginAdRevenue struct helper methods

BDNUnityPluginAdRevenue* BDNUnityPluginHelperGetAdRevenue(id<BDNAdRevenue>adRevenue) {
    if (!adRevenue) return NULL;

    BDNUnityPluginAdRevenue* unityAdRevenue = (BDNUnityPluginAdRevenue*)malloc(sizeof(BDNUnityPluginAdRevenue));
    if (!unityAdRevenue) return NULL;

    unityAdRevenue->Revenue = adRevenue.revenue;
    unityAdRevenue->RevenuePrecision = (int)adRevenue.precision;
    unityAdRevenue->Currency = CreateCString(adRevenue.currency);

    return unityAdRevenue;
}

void BDNUnityPluginHelperFreeAdRevenue(BDNUnityPluginAdRevenue* adRevenue) {
    if (!adRevenue) return;

    free((void*)adRevenue->Currency);
    free(adRevenue);
}

#pragma mark - BDNUnityPluginReward struct helper methods

BDNUnityPluginReward* BDNUnityPluginHelperGetReward(id<BDNReward>reward) {
    if (!reward) return NULL;

    BDNUnityPluginReward* unityReward = (BDNUnityPluginReward*)malloc(sizeof(BDNUnityPluginReward));
    if (!unityReward) return NULL;

    unityReward->Label = CreateCString(reward.label);
    unityReward->Amount = (double)reward.amount;

    return unityReward;
}

void BDNUnityPluginHelperFreeReward(BDNUnityPluginReward* reward) {
    if (!reward) return;

    free((void*)reward->Label);
    free(reward);
}

#pragma mark - BDNUnityPluginBannerSize struct helper methods

BDNUnityPluginBannerSize* BDNUnityPluginHelperGetBannerSize(CGSize adSize) {
    BDNUnityPluginBannerSize* bannerSize = malloc(sizeof(BDNUnityPluginBannerSize));
    if (!bannerSize) return NULL;

    bannerSize->Width = (int)adSize.width;
    bannerSize->Height = (int)adSize.height;

    return bannerSize;
}

void BDNUnityPluginHelperFreeBannerSize(BDNUnityPluginBannerSize* bannerSize) {
    if (!bannerSize) return;

    free(bannerSize);
}
