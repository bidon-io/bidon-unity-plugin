//
//  BidonRewardedAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <UnityAppController.h>
#import <BidonRewardedAdDelegate.h>
#import <BidOn/BidOn-Swift.h>

void* BDNUnityPluginCreateRewarded(const char* placement, void* delegatePtr) {
    NSString* placementNSString = [NSString stringWithUTF8String:placement];
    BDNRewardedAd* ad = [[BDNRewardedAd alloc] initWithPlacement:placementNSString];
    ad.delegate = (__bridge BDNUnityPluginRewardedAdDelegate*)delegatePtr;
    return (__bridge_retained void*)ad;
}

void BDNUnityPluginLoadRewarded(void* ptr, double priceFloor) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr loadAdWith:priceFloor];
}

bool BDNUnityPluginIsRewardedReady(void* ptr) {
    if (!ptr) return false;
    return [(__bridge BDNRewardedAd*)ptr isReady];
}

void BDNUnityPluginShowRewarded(void* ptr) {
    if (!ptr) return;
    [(__bridge BDNRewardedAd*)ptr showAdFrom:[GetAppController() rootViewController]];
}

void BDNUnityPluginDestroyRewarded(void* ptr) {
    if (!ptr) return;
    (void)(__bridge_transfer BDNRewardedAd*)ptr;
}

const char* BDNUnityPluginGetRewardedPlacementId(void* ptr) {
    if (!ptr) return nil;
    return strdup([[(__bridge BDNRewardedAd*)ptr placement] UTF8String]);
}
