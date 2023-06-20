//
//  BidonRewardedAd.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 03/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <UnityAppController.h>
#import <BidonRewardedAdDelegate.h>

void* BDNUnityPluginCreateRewarded(void* delegatePtr) {
    BDNRewardedAd* ad = [[BDNRewardedAd alloc] initWithPlacement:@"default"];
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
