//
//  BidonBannerAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <UIKit/UIKit.h>
#import <Bidon/Bidon-Swift.h>
#import <UnityAppController.h>
#import <BidonBannerAdDelegate.h>

CFBDNUnityPluginBannerAdRef BDNUnityPluginBannerAdCreate(const char* auctionKey, CFBDNUnityPluginBannerAdDelegateRef delegatePtr) {
    NSString* auctionKeyNSString = auctionKey ? [NSString stringWithUTF8String:auctionKey] : nil;
    BDNBannerProvider* ad = [[BDNBannerProvider alloc] initWithAuctionKey:auctionKeyNSString];
    ad.delegate = (__bridge BDNUnityPluginBannerAdDelegate*)delegatePtr;
    return (__bridge_retained CFBDNUnityPluginBannerAdRef)ad;
}

void BDNUnityPluginBannerAdSetFormat(CFBDNUnityPluginBannerAdRef ptr, int format) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setFormat:(BDNBannerFormat)format];
}

int BDNUnityPluginBannerAdGetFormat(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return 0;
    return (int)[(__bridge BDNBannerProvider*)ptr format];
}

BDNUnityPluginBannerSize* BDNUnityPluginBannerAdGetSize(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return NULL;
    BDNUnityPluginBannerSize* bannerSize = malloc(sizeof(BDNUnityPluginBannerSize));
    bannerSize->Width = (int)[(__bridge BDNBannerProvider*)ptr adSize].width;
    bannerSize->Height = (int)[(__bridge BDNBannerProvider*)ptr adSize].height;
    return bannerSize;
}

void BDNUnityPluginBannerAdFreeSizeStruct(BDNUnityPluginBannerSize* bannerSizeStructPtr)
{
    if (bannerSizeStructPtr) free(bannerSizeStructPtr);
}

void BDNUnityPluginBannerAdSetPredefinedPosition(CFBDNUnityPluginBannerAdRef ptr, int position) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setFixedPosition:(BDNBannerPosition)position];
}

void BDNUnityPluginBannerAdSetCustomPositionAndRotation(CFBDNUnityPluginBannerAdRef ptr,
                                                        int offsetX,
                                                        int offsetY,
                                                        int angle,
                                                        float anchorX,
                                                        float anchorY) {
    if (!ptr) return;
    float scaleFactor = [[UIScreen mainScreen] scale];
    CGPoint offset;
    offset.x = (CGFloat)offsetX / scaleFactor;
    offset.y = (CGFloat)offsetY / scaleFactor;

    CGPoint anchor;
    anchor.x = (CGFloat)anchorX;
    anchor.y = (CGFloat)anchorY;

    [(__bridge BDNBannerProvider*)ptr setCustomPosition:offset rotationAngleDegrees:(CGFloat)angle anchorPoint:anchor];
}

void BDNUnityPluginBannerAdLoad(CFBDNUnityPluginBannerAdRef ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginBannerAdIsReady(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return false;
    return [(__bridge BDNBannerProvider*)ptr isReady];
}

void BDNUnityPluginBannerAdShow(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr show];
}

bool BDNUnityPluginBannerAdIsShowing(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return false;
    return [(__bridge BDNBannerProvider*)ptr isShowing];
}

void BDNUnityPluginBannerAdHide(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr hide];
}

void BDNUnityPluginBannerAdDestroy(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNBannerProvider*)ptr;
}

void BDNUnityPluginBannerAdSetExtraDataBool(CFBDNUnityPluginBannerAdRef ptr, const char* key, bool value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithBool:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataInt(CFBDNUnityPluginBannerAdRef ptr, const char* key, int value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithInt:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataLong(CFBDNUnityPluginBannerAdRef ptr, const char* key, long value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithLong:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataFloat(CFBDNUnityPluginBannerAdRef ptr, const char* key, float value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataDouble(CFBDNUnityPluginBannerAdRef ptr, const char* key, double value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataString(CFBDNUnityPluginBannerAdRef ptr, const char* key, const char* value) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSString stringWithUTF8String:value] for:[NSString stringWithUTF8String:key]];
}

void BDNUnityPluginBannerAdSetExtraDataNull(CFBDNUnityPluginBannerAdRef ptr, const char* key) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:nil for:[NSString stringWithUTF8String:key]];
}

const char* BDNUnityPluginBannerAdGetExtraData(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return strdup([@"" UTF8String]);
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNBannerProvider*)ptr extras] options:0 error:&err];
    NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([extraDataStr UTF8String]);
}

void BDNUnityPluginBannerAdNotifyLoss(CFBDNUnityPluginBannerAdRef ptr, const char* winnerDemandId, double ecpm) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr notifyLossWithExternalDemandId:[NSString stringWithUTF8String:winnerDemandId] eCPM:ecpm];
}

void BDNUnityPluginBannerAdNotifyWin(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr notifyWin];
}
