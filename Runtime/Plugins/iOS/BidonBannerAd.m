//
//  BidonBannerAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 15/09/2023.
//

#import <BidonBannerAdDelegate.h>
#import <BidonHelperMethods.h>
#import <BidonUtilities.h>

CFBDNUnityPluginBannerAdRef BDNUnityPluginBannerAdCreate(const char* auctionKey, CFBDNUnityPluginBannerAdDelegateRef delegatePtr) {
    NSString* auctionKeyNSString = CreateNSString(auctionKey);
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
    return BDNUnityPluginHelperGetBannerSize([(__bridge BDNBannerProvider*)ptr adSize]);
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
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataInt(CFBDNUnityPluginBannerAdRef ptr, const char* key, int value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataLong(CFBDNUnityPluginBannerAdRef ptr, const char* key, long value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataFloat(CFBDNUnityPluginBannerAdRef ptr, const char* key, float value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataDouble(CFBDNUnityPluginBannerAdRef ptr, const char* key, double value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataString(CFBDNUnityPluginBannerAdRef ptr, const char* key, const char* value) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    NSString* valueNSString = CreateNSString(value);
    if (!valueNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginBannerAdSetExtraDataNull(CFBDNUnityPluginBannerAdRef ptr, const char* key) {
    if (!ptr) return;
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [(__bridge BDNBannerProvider*)ptr setExtraValue:nil for:keyNSString];
}

const char* BDNUnityPluginBannerAdGetExtraData(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return NULL;
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[(__bridge BDNBannerProvider*)ptr extras] options:0 error:&error];
    if (jsonData) {
        NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return CreateCString(extraDataStr);
    } else {
        NSLog(@"[BidonPlugin] Failed to serialize NSDictionary to JSON: %@", error.localizedDescription);
        return NULL;
    }
}

void BDNUnityPluginBannerAdNotifyLoss(CFBDNUnityPluginBannerAdRef ptr, const char* winnerDemandId, double price) {
    if (!ptr) return;
    NSString* winnerNSString = CreateNSString(winnerDemandId);
    if (!winnerNSString) return;
    [(__bridge BDNBannerProvider*)ptr notifyLossWithExternalDemandId:winnerNSString price:price];
}

void BDNUnityPluginBannerAdNotifyWin(CFBDNUnityPluginBannerAdRef ptr) {
    if (!ptr) return;
    [(__bridge BDNBannerProvider*)ptr notifyWin];
}
