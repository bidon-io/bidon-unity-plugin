//
//  BidonInterstitialAdDelegate.h
//  Bion Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <BidOn/BidOn-Swift.h>
#import <BidonUnityPluginStructs.h>

typedef void (*DidStartAuction)();
typedef void (*DidCompleteAuction)(BDNUnityPluginAd* winner);
typedef void (*DidStartAuctionRound)(const char* roundId, double priceFloor);
typedef void (*DidCompleteAuctionRound)(BDNUnityPluginAuctionRound* round);
typedef void (*DidReceiveBid)(BDNUnityPluginAd* ad);
typedef void (*DidFailToLoad)(int error);
typedef void (*DidLoad)(BDNUnityPluginAd* ad);
typedef void (*DidFailToPresent)(BDNUnityPluginImpression* impression, int error);
typedef void (*WillPresent)(BDNUnityPluginImpression* impression);
typedef void (*DidHide)(BDNUnityPluginImpression* impression);
typedef void (*DidClick)(BDNUnityPluginImpression* impression);
typedef void (*DidPayRevenue)(BDNUnityPluginAd* ad, BDNUnityPluginAdRevenue* revenue);

@interface BDNUnityPluginInterstitialAdDelegate : NSObject <BDNFullscreenAdDelegate>

@property (assign) DidStartAuction           interstitialDidStartAuctionCallback;
@property (assign) DidCompleteAuction        interstitialDidCompleteAuctionCallback;
@property (assign) DidStartAuctionRound      interstitialDidStartAuctionRoundCallback;
@property (assign) DidCompleteAuctionRound   interstitialDidCompleteAuctionRoundCallback;
@property (assign) DidReceiveBid             interstitialDidReceiveBidCallback;
@property (assign) DidFailToLoad             interstitialDidFailToLoadCallback;
@property (assign) DidLoad                   interstitialDidLoadCallback;
@property (assign) DidFailToPresent          interstitialDidFailToPresentCallback;
@property (assign) WillPresent               interstitialWillPresentCallback;
@property (assign) DidHide                   interstitialDidHideCallback;
@property (assign) DidClick                  interstitialDidClickCallback;
@property (assign) DidPayRevenue             interstitialDidPayRevenueCallback;

@end
