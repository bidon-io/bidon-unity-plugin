//
//  BidonSdkObjCBridge.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>
#import <BidonUnityPluginTypes.h>

void BDNUnityPluginSdkSetLogLevel(int logLevel) {
    [BDNSdk setLogLevel:(BDNLoggerLevel)logLevel];
}

void BDNUnityPluginSdkSetTestMode(bool isEnabled) {
    [BDNSdk setIsTestMode:isEnabled];
}

bool BDNUnityPluginSdkIsTestModeEnabled() {
    return [BDNSdk isTestMode];
}

void BDNUnityPluginSdkSetBaseUrl(const char* baseUrl) {
    NSString* baseUrlNSString = [NSString stringWithUTF8String:baseUrl];
    [BDNSdk setBaseURL:baseUrlNSString];
}

void BDNUnityPluginSdkSetExtraDataBool(const char* key, bool value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataInt(const char* key, int value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataLong(const char* key, long value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataFloat(const char* key, float value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataDouble(const char* key, double value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataString(const char* key, const char* value) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    NSString* valueNSString = [NSString stringWithUTF8String:value];
    [BDNSdk setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataNull(const char* key) {
    NSString* keyNSString = [NSString stringWithUTF8String:key];
    [BDNSdk setExtraValue:nil for:keyNSString];
}

const char* BDNUnityPluginSdkGetExtraData() {
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[BDNSdk extras] options:0 error:&err];
    NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([extraDataStr UTF8String]);
}

void BDNUnityPluginSdkRegisterDefaultAdapters() {
    [BDNSdk registerDefaultAdapters];
}

void BDNUnityPluginSdkRegisterAdapter(const char* className) {
    NSString* classNameNSString = [NSString stringWithUTF8String:className];
    [BDNSdk registerAdapterWithClassName:classNameNSString];
}

void BDNUnityPluginSdkInitialize(const char* appKey, InitializationFinishedCallback callback) {
    NSString* appKeyNSString = [NSString stringWithUTF8String:appKey];
    [BDNSdk initializeWithAppKey:appKeyNSString completion:^{
        if (callback) callback();
    }];
}

const char* BDNUnityPluginSdkGetVersion() {
    return strdup([[BDNSdk sdkVersion] UTF8String]);
}

int BDNUnityPluginSdkGetLogLevel() {
    return (int)[BDNSdk logLevel];
}

const char* BDNUnityPluginSdkGetBaseUrl() {
    return strdup([[BDNSdk baseURL] UTF8String]);
}

bool BDNUnityPluginSdkIsInitialized() {
    return [BDNSdk isInitialized];
}

void BDNUnityPluginSdkSetMetadata(const char* frameworkVersion, const char* pluginVersion) {
    NSString* frameworkVersionNSString = [NSString stringWithUTF8String:frameworkVersion];
    NSString* pluginVersionNSString = [NSString stringWithUTF8String:pluginVersion];
    [BDNSdk setFramework:BDNFrameworkUnity version:frameworkVersionNSString];
    [BDNSdk setPluginVersion:pluginVersionNSString];
}
