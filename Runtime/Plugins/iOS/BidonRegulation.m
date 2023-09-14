//
//  BidonRegulation.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 13/09/2023.
//

#import <Bidon/Bidon-Swift.h>

const char* BDNUnityPluginRegulationGetGdprConsentString() {
    if ([[BDNSdk regulations] gdprConsentString]) {
        return strdup([[[BDNSdk regulations] gdprConsentString] UTF8String]);
    }
    return strdup([@"" UTF8String]);
}

void BDNUnityPluginRegulationSetGdprConsentString(const char* gdprConsent) {
    [[BDNSdk regulations] setGdprConsentString:[NSString stringWithUTF8String:gdprConsent]];
}

const char* BDNUnityPluginRegulationGetUsPrivacyString() {
    if ([[BDNSdk regulations] usPrivacyString]) {
        return strdup([[[BDNSdk regulations] usPrivacyString] UTF8String]);
    }
    return strdup([@"" UTF8String]);
}

void BDNUnityPluginRegulationSetUsPrivacyString(const char* usPrivacy) {
    [[BDNSdk regulations] setUsPrivacyString:[NSString stringWithUTF8String:usPrivacy]];
}

int BDNUnityPluginRegulationGetGdprConsentStatus() {
    return (int)[[BDNSdk regulations] gdrpConsent];
}

void BDNUnityPluginRegulationSetGdprConsentStatus(int gdprStatus) {
    [[BDNSdk regulations] setGdrpConsent:(BDNGDPRConsentStatus)gdprStatus];
}

int BDNUnityPluginRegulationGetCoppaApplicabilityStatus() {
    return (int)[[BDNSdk regulations] coppaApplies];
}

void BDNUnityPluginRegulationSetCoppaApplicabilityStatus(int coppaStatus) {
    [[BDNSdk regulations] setCoppaApplies:(BDNCOPPAAppliesStatus)coppaStatus];
}
