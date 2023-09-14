# Interstitial Ad

> **Warning**
> <br/>For a single instance of BidonInterstitialAd, Load() and Show() can only be called once. Create a new instance for every new interstitial ad opportunity.

## Load Interstitial Ad

To load interstitial ad, first create a `BidonInterstitialAd` instance.

```c#
var bidonInter = new BidonInterstitialAd();
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

## Sending Extra Data

Note that the value has to be one of the following types: `bool`, `char`, `int`, `long`, `float`, `double`, `string`.
Passing `null` as a value will remove existing `KeyValuePair` from dictionary.

```c#
bidonInter.SetExtraData("some_key", true);
```

You can also read all current extras as shown below:

```c#
var extras = bidonInter.GetExtraData();
```

## Win / Loss Notifications

Use the following methods to let Bidon know whether or not it won the show opportunity.

```c#
bidonInter.NotifyWin();
```

```c#
bidonInter.NotifyLoss("winner_demand_id", (double)winnerEcpm);
```
