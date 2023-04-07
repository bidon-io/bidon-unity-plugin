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
