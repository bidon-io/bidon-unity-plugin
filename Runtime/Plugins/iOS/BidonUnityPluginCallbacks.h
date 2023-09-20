//
//  BidonUnityPluginCallbacks.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 20/09/20233.
//

#import <BidonUnityPluginStructs.h>

typedef void (*InitializationFinishedCallback)();

typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*DidFailToPresent)(int error);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidExpire)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);

typedef void (*DidRecordImpression)(BDNUnityPluginAd* ad);

typedef void (*WillPresent)(BDNUnityPluginAd* ad);
typedef void (*DidHide)(BDNUnityPluginAd* ad);

typedef void (*DidReceiveReward)(BDNUnityPluginAd* ad, BDNUnityPluginReward* reward);
