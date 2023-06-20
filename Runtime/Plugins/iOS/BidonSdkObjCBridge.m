//
//  BidonSdkObjCBridge.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>

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

void BDNUnityPluginSetExtraDataInt(const char* key, int value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataLong(const char* key, long value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataFloat(const char* key, float value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataDouble(const char* key, double value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataBool(const char* key, bool value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataChar(const char* key, char value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithChar:value] for:keyNSString];
}

void BDNUnityPluginSetExtraDataString(const char* key, const char* value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    NSString* valueNSString = [NSString stringWithUTF8String:value];
    [BDNSdk setExtraValue:valueNSString for:keyNSString];
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

void BDNUnityPluginSetMetadata(const char* frameworkVersion, const char* pluginVersion) {
    NSString* frameworkVersionNSString = [NSString stringWithUTF8String:frameworkVersion];
    NSString* pluginVersionNSString = [NSString stringWithUTF8String:pluginVersion];
    [BDNSdk setFramework:BDNFrameworkUnity version:frameworkVersionNSString];
    [BDNSdk setPluginVersion:pluginVersionNSString];
}
