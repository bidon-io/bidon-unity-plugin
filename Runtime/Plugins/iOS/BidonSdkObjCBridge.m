//
//  BidonSdkObjCBridge.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>

typedef void (*InitializationFinishedCallback)();

void BDNUnityPluginSetLogLevel(int logLevel) {
    [BDNSdk setLogLevel:(BDNLoggerLevel)logLevel];
}

void BDNUnityPluginSetTestMode(bool isEnabled) {
    [BDNSdk setIsTestMode:isEnabled];
}

bool BDNUnityPluginIsTestModeEnabled() {
    return [BDNSdk isTestMode];
}

void BDNUnityPluginSetBaseUrl(const char* baseUrl) {
    NSString* baseUrlNSString = [NSString stringWithUTF8String:baseUrl];
    [BDNSdk setBaseURL:baseUrlNSString];
}

void BDNUnityPluginSetExtraDataBool(const char* key, bool value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
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

void BDNUnityPluginSetExtraDataString(const char* key, const char* value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    NSString* valueNSString = [NSString stringWithUTF8String:value];
    [BDNSdk setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginSetExtraDataNull(const char* key) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:nil for:keyNSString];
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
