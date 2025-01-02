//
//  BidonInterstitialAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <UnityAppController.h>

#import <BidonInterstitialAdDelegate.h>
#import <BidonUtilities.h>

CFBDNUnityPluginInterstitialAdRef BDNUnityPluginInterstitialAdCreate(const char* auctionKey, CFBDNUnityPluginInterstitialAdDelegateRef delegatePtr) {
    NSString* auctionKeyNSString = CreateNSString(auctionKey);
    BDNInterstitial* ad = [[BDNInterstitial alloc] initWithAuctionKey:auctionKeyNSString];
    ad.delegate = (__bridge BDNUnityPluginInterstitialAdDelegate*)delegatePtr;
    return (__bridge_retained CFBDNUnityPluginInterstitialAdRef)ad;
}

void BDNUnityPluginInterstitialAdLoad(CFBDNUnityPluginInterstitialAdRef ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginInterstitialAdIsReady(CFBDNUnityPluginInterstitialAdRef ptr) {
    if (!ptr) return false;
    return [(__bridge BDNInterstitial*)ptr isReady];
}

void BDNUnityPluginInterstitialAdShow(CFBDNUnityPluginInterstitialAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr showAdFrom:[GetAppController() rootViewController]];
}

void BDNUnityPluginInterstitialAdDestroy(CFBDNUnityPluginInterstitialAdRef ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNInterstitial*)ptr;
}

void BDNUnityPluginInterstitialAdSetExtraDataBool(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, bool value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataInt(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, int value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataLong(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, long value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataFloat(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, float value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataDouble(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, double value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataString(CFBDNUnityPluginInterstitialAdRef ptr, const char* key, const char* value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    NSString* valueNSString = CreateNSString(value);
    if (!valueNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginInterstitialAdSetExtraDataNull(CFBDNUnityPluginInterstitialAdRef ptr, const char* key) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:nil for:keyNSString];
}

const char* BDNUnityPluginInterstitialAdGetExtraData(CFBDNUnityPluginInterstitialAdRef ptr) {
    if (!ptr) return NULL;
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNInterstitial*)ptr extras] options:0 error:&error];
    if (jsonData) {
        NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return CreateCString(extraDataStr);
    } else {
        NSLog(@"[BidonPlugin] Failed to serialize NSDictionary to JSON: %@", error.localizedDescription);
        return NULL;
    }
}

void BDNUnityPluginInterstitialAdNotifyLoss(CFBDNUnityPluginInterstitialAdRef ptr, const char* winnerDemandId, double price) {
    if (!ptr) return;
    NSString* winnerNSString = CreateNSString(winnerDemandId);
    if (!winnerNSString) return;
    [(__bridge BDNInterstitial*)ptr notifyLossWithExternalDemandId:winnerNSString price:price];
}

void BDNUnityPluginInterstitialAdNotifyWin(CFBDNUnityPluginInterstitialAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr notifyWin];
}
