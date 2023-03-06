//
//  BidonInterstitialAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <UnityAppController.h>
#import <BidonInterstitialAdDelegate.h>
#import <BidOn/BidOn-Swift.h>

void* BDNUnityPluginCreateInterstitial(const char* placement, void* delegatePtr) {
    NSString* placementNSString = [NSString stringWithUTF8String:placement];
    BDNInterstitial* ad = [[BDNInterstitial alloc] initWithPlacement:placementNSString];
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

const char* BDNUnityPluginGetInterstitialPlacementId(void* ptr) {
    if (!ptr) return nil;
    return strdup([[(__bridge BDNInterstitial*)ptr placement] UTF8String]);
}
