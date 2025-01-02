//
//  BidonSdkObjCBridge.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/03/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUnityPluginTypes.h>
#import <BidonUtilities.h>

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
    NSString* baseUrlNSString = CreateNSString(baseUrl);
    if (!baseUrlNSString) return;
    [BDNSdk setBaseURL:baseUrlNSString];
}

void BDNUnityPluginSdkSetExtraDataBool(const char* key, bool value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:[NSNumber numberWithBool:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataInt(const char* key, int value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:[NSNumber numberWithInt:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataLong(const char* key, long value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:[NSNumber numberWithLong:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataFloat(const char* key, float value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:[NSNumber numberWithFloat:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataDouble(const char* key, double value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:[NSNumber numberWithDouble:value] for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataString(const char* key, const char* value) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    NSString* valueNSString = CreateNSString(value);
    if (!valueNSString) return;
    [BDNSdk setExtraValue:valueNSString for:keyNSString];
}

void BDNUnityPluginSdkSetExtraDataNull(const char* key) {
    NSString* keyNSString = CreateNSString(key);
    if (!keyNSString) return;
    [BDNSdk setExtraValue:nil for:keyNSString];
}

const char* BDNUnityPluginSdkGetExtraData() {
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[BDNSdk extras] options:0 error:&error];
    if (jsonData) {
        NSString* extraDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return CreateCString(extraDataStr);
    } else {
        NSLog(@"[BidonPlugin] Failed to serialize NSDictionary to JSON: %@", error.localizedDescription);
        return NULL;
    }
}

void BDNUnityPluginSdkRegisterDefaultAdapters() {
    [BDNSdk registerDefaultAdapters];
}

void BDNUnityPluginSdkRegisterAdapter(const char* className) {
    NSString* classNameNSString = CreateNSString(className);
    if (!classNameNSString) return;
    [BDNSdk registerAdapterWithClassName:classNameNSString];
}

void BDNUnityPluginSdkInitialize(const char* appKey, InitializationFinishedCallback callback) {
    NSString* appKeyNSString = CreateNSString(appKey);
    if (!appKeyNSString) return;
    [BDNSdk initializeWithAppKey:appKeyNSString completion:^{
        if (callback) callback();
    }];
}

const char* BDNUnityPluginSdkGetVersion() {
    return CreateCString([BDNSdk sdkVersion]);
}

int BDNUnityPluginSdkGetLogLevel() {
    return (int)[BDNSdk logLevel];
}

const char* BDNUnityPluginSdkGetBaseUrl() {
    return CreateCString([BDNSdk baseURL]);
}

bool BDNUnityPluginSdkIsInitialized() {
    return [BDNSdk isInitialized];
}

void BDNUnityPluginSdkSetMetadata(const char* frameworkVersion, const char* pluginVersion) {
    NSString* frameworkVersionNSString = CreateNSString(frameworkVersion);
    if (!frameworkVersionNSString) return;
    NSString* pluginVersionNSString = CreateNSString(pluginVersion);
    if (!pluginVersionNSString) return;
    [BDNSdk setFramework:BDNFrameworkUnity version:frameworkVersionNSString];
    [BDNSdk setPluginVersion:pluginVersionNSString];
}
