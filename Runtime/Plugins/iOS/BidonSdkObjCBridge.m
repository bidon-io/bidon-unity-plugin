//
//  BidonSdkObjCBridge.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <BidOn/BidOn-Swift.h>

typedef void (*InitializationFinishedCallback)();

void BDNUnityPluginSetLogLevel(int logLevel) {
    switch (logLevel) {
        case 0:
            [BDNSdk setLogLevel:BDNLoggerLevelVerbose];
            break;
        case 1:
            [BDNSdk setLogLevel:BDNLoggerLevelDebug];
            break;
        case 2:
            [BDNSdk setLogLevel:BDNLoggerLevelInfo];
            break;
        case 3:
            [BDNSdk setLogLevel:BDNLoggerLevelWarning];
            break;
        case 4:
            [BDNSdk setLogLevel:BDNLoggerLevelError];
            break;
        case 5:
            [BDNSdk setLogLevel:BDNLoggerLevelOff];
            break;
        default:
            break;
    }
}

void BDNUnityPluginSetBaseUrl(const char* baseUrl) {
    NSString* baseUrlNSString = [NSString stringWithUTF8String:baseUrl];
    [BDNSdk setBaseURL:baseUrlNSString];
}

void BDNUnityPluginRegisterDefaultAdapters() {
    [BDNSdk registerDefaultAdapters];
}

void BDNUnityPluginRegisterAdapter(const char* className) {
    NSString* classNameNSString = [NSString stringWithUTF8String:className];
    [BDNSdk registerAdapterWithClassName:classNameNSString];
}

void BDNUnityPluginInitialize(const char* appKey, InitializationFinishedCallback callback) {
    NSString* appKeyNSString = [NSString stringWithUTF8String:appKey];
    [BDNSdk initializeWithAppKey:appKeyNSString completion:^{
        if (callback) callback();
    }];
}

const char* BDNUnityPluginGetSdkVersion() {
    return strdup([[BDNSdk sdkVersion] UTF8String]);
}

int BDNUnityPluginGetLogLevel() {
    return (int)[BDNSdk logLevel];
}

const char* BDNUnityPluginGetBaseUrl() {
    return strdup([[BDNSdk baseURL] UTF8String]);
}

bool BDNUnityPluginIsInitialized() {
    return [BDNSdk isInitialized];
}
