# Integration

This page describes how to import and configure Bidon Unity Plugin. 

- [Getting Started](#getting-started) 
- [Initialize the SDK](#initialize-the-sdk)
- [Configure Ad Types](#configure-ad-types)

## Getting Started 

1. Download External Dependency Manager v1.2.175 or newer from [this website](https://developers.google.com/unity/archive#external_dependency_manager_for_unity).
2. Import EDM into your Unity project by adding downloaded archive via Unity Package Manager (`Window -> Package Manager -> "+" -> Add package from tarball`).
3. Copy the link from below and install Bidon Unity Plugin via UPM (`Window -> Package Manager -> "+" -> Add package from git URL`).

```
https://github.com/bidon-io/bidon-unity-plugin.git
```

## Initialize the SDK

Get your `APP_KEY` from dashboard' app settings.

Add Bidon namespace to your script.

```c#
using Bidon.Mediation;
```

We recommend to initialize Bidon SDK on app launch in Start() method.

```c#
private void Start()
{
    BidonSdk.Instance.SetLogLevel(BidonLogLevel.Verbose);
    BidonSdk.Instance.RegisterDefaultAdapters();
    BidonSdk.Instance.OnInitializationFinished += (sender, args) =>
    {
        Debug.Log($"Is Initialized: {BidonSdk.Instance.IsInitialized()}");
    };
    BidonSdk.Instance.Initialize("APP_KEY");
}
```

## Configure Ad Types

- [Interstitial](ad-formats/Interstitial.md)
- [Rewarded](ad-formats/Rewarded.md)
