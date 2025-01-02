//
//  BidonInterstitialAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUnityPluginTypes.h>

@interface BDNUnityPluginInterstitialAdDelegate : NSObject <BDNFullscreenAdDelegate>

@property (nonatomic, assign) DidLoad              interstitialDidLoadCallback;
@property (nonatomic, assign) DidFailToLoad        interstitialDidFailToLoadCallback;
@property (nonatomic, assign) WillPresent          interstitialWillPresentCallback;
@property (nonatomic, assign) DidFailToPresent     interstitialDidFailToPresentCallback;
@property (nonatomic, assign) DidClick             interstitialDidClickCallback;
@property (nonatomic, assign) DidHide              interstitialDidHideCallback;
@property (nonatomic, assign) DidExpire            interstitialDidExpireCallback;
@property (nonatomic, assign) DidPayRevenue        interstitialDidPayRevenueCallback;

@end
