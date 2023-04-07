//
//  BidonUnityPluginStructs.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

typedef struct {
    const char* AdUnitId;
    const char* AuctionId;
    const char* CurrencyCode;
    int         AdType;
    const char* Dsp;
    double      Ecpm;
    const char* NetworkName;
    const char* RoundId;
} BDNUnityPluginAd;

typedef struct {
    double      Revenue;
    int         RevenuePrecision;
    const char* Currency;
} BDNUnityPluginAdRevenue;

typedef struct {
    const char* label;
    double      amount;
} BDNUnityPluginReward;
