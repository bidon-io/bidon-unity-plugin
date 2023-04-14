//
//  BidonInterstitialAdDelegate.h
//  Bion Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginStructs.h>

typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*WillPresent)(BDNUnityPluginAd* ad);
typedef void (*DidFailToPresent)(int error);
typedef void (*DidClick)(BDNUnityPluginAd* ad);
typedef void (*DidHide)(BDNUnityPluginAd* ad);
typedef void (*DidExpire)(BDNUnityPluginAd* ad);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);

@interface BDNUnityPluginInterstitialAdDelegate : NSObject <BDNFullscreenAdDelegate>

@property (assign) DidLoad              interstitialDidLoadCallback;
@property (assign) DidFailToLoad        interstitialDidFailToLoadCallback;
@property (assign) WillPresent          interstitialWillPresentCallback;
@property (assign) DidFailToPresent     interstitialDidFailToPresentCallback;
@property (assign) DidClick             interstitialDidClickCallback;
@property (assign) DidHide              interstitialDidHideCallback;
@property (assign) DidExpire            interstitialDidExpireCallback;
@property (assign) DidPayRevenue        interstitialDidPayRevenueCallback;

@end
