//
//  BidonRewardedAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <UnityAppController.h>
#import <BidonRewardedAdDelegate.h>

CFBDNUnityPluginRewardedAdRef BDNUnityPluginRewardedAdCreate(CFBDNUnityPluginRewardedAdDelegateRef delegatePtr) {
    BDNRewardedAd* ad = [[BDNRewardedAd alloc] initWithPlacement:@"default"];
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
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithBool:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataInt(CFBDNUnityPluginRewardedAdRef ptr, const char* key, int value) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithInt:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataLong(CFBDNUnityPluginRewardedAdRef ptr, const char* key, long value) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithLong:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataFloat(CFBDNUnityPluginRewardedAdRef ptr, const char* key, float value) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataDouble(CFBDNUnityPluginRewardedAdRef ptr, const char* key, double value) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataString(CFBDNUnityPluginRewardedAdRef ptr, const char* key, const char* value) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:[NSString stringWithUTF8String:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginRewardedAdSetExtraDataNull(CFBDNUnityPluginRewardedAdRef ptr, const char* key) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr setExtraValue:nil for:[NSString stringWithUTF8String:key]];
}

const char* BDNUnityPluginRewardedAdGetExtraData(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return strdup([@"" UTF8String]);
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNRewardedAd*)ptr extras] options:0 error:&err];
    NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([extraDataStr UTF8String]);
}

void BDNUnityPluginRewardedAdNotifyLoss(CFBDNUnityPluginRewardedAdRef ptr, const char* winnerDemandId, double ecpm) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr notifyLossWithExternalDemandId:[NSString stringWithUTF8String:winnerDemandId] eCPM:ecpm];
}

void BDNUnityPluginRewardedAdNotifyWin(CFBDNUnityPluginRewardedAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr notifyWin];
}
