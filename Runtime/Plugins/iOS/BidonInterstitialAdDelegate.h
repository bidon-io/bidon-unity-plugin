//
//  BidonInterstitialAdDelegate.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonHelperMethods.h>

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
