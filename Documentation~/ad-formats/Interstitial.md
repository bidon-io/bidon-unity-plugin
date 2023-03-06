# Interstitial Ad

> **Warning**
> For a single instance of BidonInterstitialAd, Load() and Show() can only be called once. Create a new instance for every new interstitial ad opportunity.

## Load Interstitial Ad

To load interstitial ad, first create a `BidonInterstitialAd` instance. Specify placement (only if needed), otherwise placement = "default" will be used.

```c#
var bidonInter = new BidonInterstitialAd("PLACEMENT_NAME");
```

Subscribe to desired events for receiving callbacks during ad object lifecycle.

```c#
bidonInter.OnAdLoaded += (sender, args) =>
{
    Debug.Log($"Ecpm: {args.Ad?.Ecpm}");
};
```

Below you can find all available events for interstitials.

<details>
<summary>Click to expand</summary>
<p>

```c#
event EventHandler<BidonAuctionStartedEventArgs> OnAuctionStarted;
event EventHandler<BidonAuctionSucceedEventArgs> OnAuctionSucceed;
event EventHandler<BidonAuctionFailedEventArgs> OnAuctionFailed;

event EventHandler<BidonRoundStartedEventArgs> OnRoundStarted;
event EventHandler<BidonRoundSucceedEventArgs> OnRoundSucceed;
event EventHandler<BidonRoundFailedEventArgs> OnRoundFailed;

event EventHandler<BidonAdLoadedEventArgs> OnAdLoaded;
event EventHandler<BidonAdLoadFailedEventArgs> OnAdLoadFailed;
event EventHandler<BidonAdShownEventArgs> OnAdShown;
event EventHandler<BidonAdShowFailedEventArgs> OnAdShowFailed;
event EventHandler<BidonAdClickedEventArgs> OnAdClicked;
event EventHandler<BidonAdClosedEventArgs> OnAdClosed;
event EventHandler<BidonAdExpiredEventArgs> OnAdExpired;

event EventHandler<BidonAdRevenueReceivedEventArgs> OnAdRevenueReceived;
```

</p>
</details>

Load an interstitial ad.

```c#
bidonInter.Load(0.01d);
```

## Display Interstitial Ad

```c#
if (bidonInter.IsReady())
{
    bidonInter.Show();
}
```

## Destroy Interstitial Ad

To release resources we recommend destroying instances when you don't need them anymore.

```c#
bidonInter.Destroy();
bidonInter = null;
```
