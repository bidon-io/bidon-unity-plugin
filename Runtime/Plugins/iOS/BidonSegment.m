//
//  BidonSegment.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 12/09/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUtilities.h>

const char* BDNUnityPluginSegmentGetUid() {
    return CreateCString([[BDNSdk segment] uid]);
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
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:[[BDNSdk segment] customAttributes] options:0 error:&error];
    if (jsonData) {
        NSString* attributesStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return CreateCString(attributesStr);
    } else {
        NSLog(@"[BidonPlugin] Failed to serialize NSDictionary to JSON: %@", error.localizedDescription);
        return NULL;
    }
}

void BDNUnityPluginSegmentSetCustomAttributeBool(const char* name, bool value) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithBool:value] for:nameNSString];
}

void BDNUnityPluginSegmentSetCustomAttributeInt(const char* name, int value) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithInt:value] for:nameNSString];
}

void BDNUnityPluginSegmentSetCustomAttributeLong(const char* name, long value) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithLong:value] for:nameNSString];
}

void BDNUnityPluginSegmentSetCustomAttributeDouble(const char* name, double value) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    [[BDNSdk segment] setCustomAttribute:[NSNumber numberWithDouble:value] for:nameNSString];
}

void BDNUnityPluginSegmentSetCustomAttributeString(const char* name, const char* value) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    NSString* valueNSString = CreateNSString(value);
    if (!valueNSString) return;
    [[BDNSdk segment] setCustomAttribute:valueNSString for:nameNSString];
}

void BDNUnityPluginSegmentSetCustomAttributeNull(const char* name) {
    NSString* nameNSString = CreateNSString(name);
    if (!nameNSString) return;
    [[BDNSdk segment] setCustomAttribute:nil for:nameNSString];
}
