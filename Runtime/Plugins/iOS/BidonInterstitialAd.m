//
//  BidonInterstitialAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <UnityAppController.h>
#import <BidonInterstitialAdDelegate.h>

void* BDNUnityPluginCreateInterstitial(void* delegatePtr) {
    BDNInterstitial* ad = [[BDNInterstitial alloc] initWithPlacement:@"default"];
    ad.delegate = (__bridge BDNUnityPluginInterstitialAdDelegate*)delegatePtr;
    return (__bridge_retained void*)ad;
}

void BDNUnityPluginLoadInterstitial(void* ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginIsInterstitialReady(void* ptr) {
    if (!ptr) return false;
    return [(__bridge BDNInterstitial*)ptr isReady];
}

void BDNUnityPluginShowInterstitial(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr showAdFrom:[GetAppController() rootViewController]];
}

void BDNUnityPluginDestroyInterstitial(void* ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNInterstitial*)ptr;
}

void BDNUnityPluginInterstitialAdSetExtraDataBool(void* ptr, const char* key, bool value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithBool:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataInt(void* ptr, const char* key, int value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithInt:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataLong(void* ptr, const char* key, long value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithLong:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataFloat(void* ptr, const char* key, float value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataDouble(void* ptr, const char* key, double value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataString(void* ptr, const char* key, const char* value) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:[NSString stringWithUTF8String:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginInterstitialAdSetExtraDataNull(void* ptr, const char* key) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr setExtraValue:nil for:[NSString stringWithUTF8String:key]];
}

const char* BDNUnityPluginInterstitialAdGetExtraData(void* ptr) {
    if (!ptr) return strdup([@"" UTF8String]);
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNInterstitial*)ptr extras] options:0 error:&err];
    NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([extraDataStr UTF8String]);
}

void BDNUnityPluginInterstitialAdNotifyLoss(void* ptr, const char* winnerDemandId, double ecpm) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr notifyLossWithExternalDemandId:[NSString stringWithUTF8String:winnerDemandId] eCPM:ecpm];
}

void BDNUnityPluginInterstitialAdNotifyWin(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNInterstitial*)ptr notifyWin];
}
