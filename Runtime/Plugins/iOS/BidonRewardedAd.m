//
//  BidonRewardedAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <UnityAppController.h>

#import <BidonRewardedAdDelegate.h>
#import <BidonUtilities.h>

CFBDNUnityPluginRewardedAdRef BDNUnityPluginRewardedAdCreate(const char* auctionKey, CFBDNUnityPluginRewardedAdDelegateRef delegatePtr) {
    NSString* auctionKeyNSString = CreateNSString(auctionKey);
    BDNRewardedAd* ad = [[BDNRewardedAd alloc] initWithAuctionKey:auctionKeyNSString];
    ad.delegate = (__bridge BDNUnityPluginRewardedAdDelegate*)delegatePtr;
    return (__bridge_retained CFBDNUnityPluginRewardedAdRef)ad;
}

void BDNUnityPluginRewardedAdLoad(CFBDNUnityPluginRewardedAdRef ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginRewardedAdIsReady(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return false;
    return [(__bridge BDNRewardedAd*)ptr isReady];
}

void BDNUnityPluginRewardedAdShow(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr showAdFrom:[GetAppController() rootViewController]];
}

void BDNUnityPluginRewardedAdDestroy(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNRewardedAd*)ptr;
}

void BDNUnityPluginRewardedAdSetExtraDataBool(CFBDNUnityPluginRewardedAdRef ptr, const char* key, bool value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataInt(CFBDNUnityPluginRewardedAdRef ptr, const char* key, int value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataLong(CFBDNUnityPluginRewardedAdRef ptr, const char* key, long value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataFloat(CFBDNUnityPluginRewardedAdRef ptr, const char* key, float value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataDouble(CFBDNUnityPluginRewardedAdRef ptr, const char* key, double value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataString(CFBDNUnityPluginRewardedAdRef ptr, const char* key, const char* value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    NSString* valueNSString = CreateNSString(value);
    if (!valueNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginRewardedAdSetExtraDataNull(CFBDNUnityPluginRewardedAdRef ptr, const char* key) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:nil for:keyNSString];
}

const char* BDNUnityPluginRewardedAdGetExtraData(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return NULL;
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNRewardedAd*)ptr extras] options:0 error:&error];
    if (jsonData) {
        NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return CreateCString(extraDataStr);
    } else {
        NSLog(@"[BidonPlugin] Failed to serialize NSDictionary to JSON: %@", error.localizedDescription);
        return NULL;
    }
}

void BDNUnityPluginRewardedAdNotifyLoss(CFBDNUnityPluginRewardedAdRef ptr, const char* winnerDemandId, double price) {
    if (!ptr) return;
    NSString* winnerNSString = CreateNSString(winnerDemandId);
    if (!winnerNSString) return;
    [(__bridge BDNRewardedAd*)ptr notifyLossWithExternalDemandId:winnerNSString price:price];
}

void BDNUnityPluginRewardedAdNotifyWin(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr notifyWin];
}
