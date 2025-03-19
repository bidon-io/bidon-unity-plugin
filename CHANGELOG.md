# Bidon Unity Plugin

## Changelog

### v0.7.0 (March 19, 2025)

+ Updated Bidon Android SDK to v0.7.6
+ Updated Bidon iOS SDK to v0.7.12
+ Replaced `BidonSegment.Id` field with `BidonSegment.Uid`
+ Changed `BidonAd` dto class fields
+ Renamed `BidonGdprConsentStatus` enum to `BidonGdprApplicabilityStatus`
+ Implemented `GetSize` method for `BidonBannerAd` class
+ Added `IncorrectAdUnit` status to `BidonError` enum
+ Added optional `auctionKey` param to `BidonAd` constructors
+ Implemented `AuctionInfo` param support for `OnAdLoaded` and `OnAdLoadFailed` callbacks
+ Added `BidonConstants.AdapterNames` constants
+ Made `priceFloor` parameter optional in `Load` methods for all ad types
+ Refactored Android & iOS SDK bridges
+ Refactored Editor scripts
+ Added other minor improvements

### v0.4.5 (January 16, 2024)

+ Updated Bidon Android SDK to v0.4.28
+ Updated Bidon iOS SDK to v0.4.7

### v0.4.4 (December 07, 2023)

+ Updated Bidon Android SDK to v0.4.26
+ Updated Bidon iOS SDK to v0.4.6
+ Added Bidon GAM adapter

### v0.4.3 (November 24, 2023)

+ Updated Bidon Android SDK to v0.4.25
+ Updated Bidon iOS SDK to v0.4.5

### v0.4.2 (November 14, 2023)

+ Updated Bidon Android SDK to v0.4.23
+ Added Bidon MobileFuse adapter for Android SDK

### v0.4.1 (November 9, 2023)

+ Updated Bidon Android SDK to v0.4.21
+ Updated Bidon iOS SDK to v0.4.4
+ Added Amazon Adapter
+ Removed `allowBackup` attribute from android manifest
+ Changed GDPR enum values for android bridge
+ Tuned up dismiss callbacks on iOS

### v0.4.0 (September 26, 2023)

+ Updated Bidon Android SDK to v0.4.18
+ Updated Bidon iOS SDK to v0.4.3
+ Added Banner ad type support
+ Reworked sample project
+ Improved iOS bridge
+ Tuned up GC

### v0.3.0 (September 14, 2023)

+ Updated Bidon Android SDK to v0.3.2
+ Updated Bidon iOS SDK to v0.3.3
+ Implemented `Segments` API
+ Implemented `Regulations` API
+ Added `SetTestMode()` & `IsTestModeEnabled()` methods to `BidonSdk` class
+ Added `SetExtraData()`, `GetExtraData()`, `NotifyLoss()`, and `NotifyWin()` methods to `AdObject` classes

### v0.2.1 (June 20, 2023)

+ Updated Bidon Android SDK to v0.2.1
+ Updated Bidon iOS SDK to v0.2.1
+ Changed minimal supported Unity version to v2020.1
+ Added `SetExtraData` method
+ Removed placements and auction callbacks logic
+ Updated iOS bridge (synced callbacks with android platform)
+ Implemented some internal improvements

### v0.1.0 (March 16, 2023)

+ Integrated Bidon Android SDK v0.1.1-Beta
+ Integrated Bidon iOS SDK v0.1.3.1-Beta
+ Prepared package for UPM distribution
+ Implemented necessary plugin tools
+ Implemented plugin API
+ Implemented Android bridge
+ Implemented iOS bridge
+ Created usage sample
