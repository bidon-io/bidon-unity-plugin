//
//  BidonUnityPluginStructs.h
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

typedef struct {
    const char* Id;
    double      Ecpm;
    const char* AdUnitId;
    const char* NetworkName;
    const char* Dsp;
} BDNUnityPluginAd;

typedef struct {
    const char* RoundId;
    const char* Demands;
    double      Timeout;
} BDNUnityPluginAuctionRound;

typedef struct {
    const char*         ImpressionId;
    const char*         AuctionId;
    int                 AuctionConfigurationId;
    BDNUnityPluginAd*   ad;
    double              showTrackTime;
    double              clickTrackTime;
    double              rewardTrackTime;
} BDNUnityPluginImpression;

typedef struct {
    double      Revenue;
    int         RevenuePrecision;
    const char* Currency;
    
} BDNUnityPluginAdRevenue;

typedef struct {
    const char* label;
    double      amount;
} BDNUnityPluginReward;
