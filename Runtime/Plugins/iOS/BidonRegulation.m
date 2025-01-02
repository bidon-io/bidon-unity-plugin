//
//  BidonRegulation.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 13/09/2023.
//

#import <Bidon/Bidon-Swift.h>

#import <BidonUtilities.h>

int BDNUnityPluginRegulationGetGdprApplicabilityStatus() {
    return (int)[[BDNSdk regulations] gdpr];
}

void BDNUnityPluginRegulationSetGdprApplicabilityStatus(int gdprStatus) {
    [[BDNSdk regulations] setGdpr:(BDNGDPRAppliesStatus)gdprStatus];
}

const char* BDNUnityPluginRegulationGetGdprConsentString() {
    NSString* consentString = [[BDNSdk regulations] gdprConsentString];
    return CreateCString(consentString);
}

void BDNUnityPluginRegulationSetGdprConsentString(const char* gdprConsent) {
    NSString* gdprConsentNSString = CreateNSString(gdprConsent);
    [[BDNSdk regulations] setGdprConsentString:gdprConsentNSString];
}

bool BDNUnityPluginRegulationGetIsGdprApplied() {
    return [[BDNSdk regulations] gdprApplies];
}

bool BDNUnityPluginRegulationGetHasGdprConsent() {
    return [[BDNSdk regulations] hasGdprConsent];
}

const char* BDNUnityPluginRegulationGetUsPrivacyString() {
    NSString* privacyString = [[BDNSdk regulations] usPrivacyString];
    return CreateCString(privacyString);
}

void BDNUnityPluginRegulationSetUsPrivacyString(const char* usPrivacy) {
    NSString* usPrivacyNSString = CreateNSString(usPrivacy);
    [[BDNSdk regulations] setUsPrivacyString:usPrivacyNSString];
}

bool BDNUnityPluginRegulationGetIsCcpaApplied() {
    return [[BDNSdk regulations] ccpaApplies];
}

bool BDNUnityPluginRegulationGetHasCcpaConsent() {
    return [[BDNSdk regulations] hasCcpaConsent];
}

int BDNUnityPluginRegulationGetCoppaApplicabilityStatus() {
    return (int)[[BDNSdk regulations] coppa];
}

void BDNUnityPluginRegulationSetCoppaApplicabilityStatus(int coppaStatus) {
    [[BDNSdk regulations] setCoppa:(BDNCOPPAAppliesStatus)coppaStatus];
}

bool BDNUnityPluginRegulationGetIsCoppaApplied() {
    return [[BDNSdk regulations] coppaApplies];
}
