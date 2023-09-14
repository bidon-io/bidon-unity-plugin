//
//  BidonSegment.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 12/09/2023.
//

#import <Bidon/Bidon-Swift.h>

const char* BDNUnityPluginSegmentGetId() {
    if ([[BDNSdk segment] id]) {
        return strdup([[[BDNSdk segment] id] UTF8String]);
    }
    return strdup([@"" UTF8String]);
}

int BDNUnityPluginSegmentGetAge() {
    return (int)[[BDNSdk segment] age];
}

void BDNUnityPluginSegmentSetAge(int age) {
    [[BDNSdk segment] setAge:age];
}

int BDNUnityPluginSegmentGetGender() {
    return (int)[[BDNSdk segment] gender];
}

void BDNUnityPluginSegmentSetGender(int gender) {
    [[BDNSdk segment] setGender:(BDNGender)gender];
}

int BDNUnityPluginSegmentGetLevel() {
    return (int)[[BDNSdk segment] level];
}

void BDNUnityPluginSegmentSetLevel(int level) {
    [[BDNSdk segment] setLevel:level];
}

double BDNUnityPluginSegmentGetTotalInAppsAmount() {
    return [[BDNSdk segment] inAppAmount];
}

void BDNUnityPluginSegmentSetTotalInAppsAmount(double inAppsAmount) {
    [[BDNSdk segment] setInAppAmount:inAppsAmount];
}

bool BDNUnityPluginSegmentGetIsPaying() {
    return [[BDNSdk segment] isPaid];
}

void BDNUnityPluginSegmentSetIsPaying(bool isPaying) {
    [[BDNSdk segment] setIsPaid:isPaying];
}

const char* BDNUnityPluginSegmentGetCustomAttributes() {
    NSError* err;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[[BDNSdk segment] customAttributes] options:0 error:&err];
    NSString* attributesStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([attributesStr UTF8String]);
}

void BDNUnityPluginSegmentSetCustomAttributeBool(const char* name, bool value) {
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithBool:value] for:[NSString stringWithUTF8String:name]];
}

void BDNUnityPluginSegmentSetCustomAttributeInt(const char* name, int value) {
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithInt:value] for:[NSString stringWithUTF8String:name]];
}

void BDNUnityPluginSegmentSetCustomAttributeLong(const char* name, long value) {
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithLong:value] for:[NSString stringWithUTF8String:name]];
}

void BDNUnityPluginSegmentSetCustomAttributeDouble(const char* name, double value) {
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithDouble:value] for:[NSString stringWithUTF8String:name]];
}

void BDNUnityPluginSegmentSetCustomAttributeString(const char* name, const char* value) {
    [[BDNSdk segment] setCustomAttribute:[NSString stringWithUTF8String:value] for:[NSString stringWithUTF8String:name]];
}

void BDNUnityPluginSegmentSetCustomAttributeNull(const char* name) {
    [[BDNSdk segment] setCustomAttribute:nil for:[NSString stringWithUTF8String:name]];
}
