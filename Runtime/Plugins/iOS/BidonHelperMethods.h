//
//  BidonHelperMethods.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 07/04/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginTypes.h>

extern BDNUnityPluginAd BDNUnityPluginHelperGetAd(id<BDNAd>ad);

extern BDNUnityPluginAuctionInfo BDNUnityPluginHelperGetAuctionInfo(id<BDNAuctionInfo>auctionInfo);

extern BDNUnityPluginAdRevenue BDNUnityPluginHelperGetAdRevenue(id<BDNAdRevenue>revenue);

extern BDNUnityPluginReward BDNUnityPluginHelperGetReward(id<BDNReward>reward);
