//
//  BidonBannerAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <UnityAppController.h>
#import <BidonBannerAdDelegate.h>

void* BDNUnityPluginBannerAdCreateInstance(void* delegatePtr) {
    BDNBannerProvider* ad = [[BDNBannerProvider alloc] init];
    ad.delegate = (__bridge BDNUnityPluginBannerAdDelegate*)delegatePtr;
    return (__bridge_retained void*)ad;
}

void BDNUnityPluginBannerAdSetFormat(void* ptr, int format) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setFormat:(BDNBannerFormat)format];
}

void BDNUnityPluginBannerAdSetPredefinedPosition(void* ptr, int position) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setFixedPosition:(BDNBannerPosition)position];
}

void BDNUnityPluginBannerAdSetCustomPositionAndRotation(void* ptr,
                                                        int offsetX,
                                                        int offsetY,
                                                        int angle,
                                                        float anchorX,
                                                        float anchorY) {
    if (!ptr) return;
    CGPoint offset;
    offset.x = (CGFloat)offsetX;
    offset.y = (CGFloat)offsetY;

    CGPoint anchor;
    anchor.x = (CGFloat)anchorX;
    anchor.y = (CGFloat)anchorY;

    [(__bridge BDNBannerProvider*)ptr setCustomPosition:offset rotationAngleDegrees:(CGFloat)angle anchorPoint:anchor];
}

void BDNUnityPluginBannerAdLoad(void* ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginBannerAdIsReady(void* ptr) {
    if (!ptr) return false;
    return [(__bridge BDNBannerProvider*)ptr isReady];
}

void BDNUnityPluginBannerAdShow(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr show];
}

void BDNUnityPluginBannerAdHide(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr hide];
}

void BDNUnityPluginBannerAdDestroyInstance(void* ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNBannerProvider*)ptr;
}

void BDNUnityPluginBannerAdSetExtraDataBool(void* ptr, const char* key, bool value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithBool:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataInt(void* ptr, const char* key, int value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithInt:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataLong(void* ptr, const char* key, long value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithLong:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataFloat(void* ptr, const char* key, float value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataDouble(void* ptr, const char* key, double value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataString(void* ptr, const char* key, const char* value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSString stringWithUTF8String:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataNull(void* ptr, const char* key) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:nil for:[NSString stringWithUTF8String:key]];
}

const char* BDNUnityPluginBannerAdGetExtraData(void* ptr) {
    if (!ptr) return strdup([@"" UTF8String]);
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNBannerProvider*)ptr extras] options:0 error:&err];
    NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([extraDataStr UTF8String]);
}

void BDNUnityPluginBannerAdNotifyLoss(void* ptr, const char* winnerDemandId, double ecpm) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr notifyLossWithExternalDemandId:[NSString stringWithUTF8String:winnerDemandId] eCPM:ecpm];
}

void BDNUnityPluginBannerAdNotifyWin(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr notifyWin];
}
