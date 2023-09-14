# Rewarded Ad

> **Warning**
> <br/>For a single instance of BidonRewardedAd, Load() and Show() can only be called once. Create a new instance for every new rewarded ad opportunity.

## Load Rewarded Ad

To load rewarded ad, first create a `BidonRewardedAd` instance.

```c#
var bidonRewarded = new BidonRewardedAd();
```

Subscribe to desired events for receiving callbacks during ad object lifecycle.

```c#
bidonRewarded.OnAdLoaded += (sender, args) =>
{
    Debug.Log($"Ecpm: {args.Ad?.Ecpm}");
};
```

Below you can find all available events for rewarded ads.

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
event EventHandler<BidonUserRewardedEventArgs> OnUserRewarded;
```

</p>
</details>

Load a rewarded ad.

```c#
bidonRewarded.Load(0.01d);
```

## Display Rewarded Ad

```c#
if (bidonRewarded.IsReady())
{
    bidonRewarded.Show();
}
```

## Destroy Rewarded Ad

To release resources we recommend destroying instances when you don't need them anymore.

```c#
bidonRewarded.Destroy();
bidonRewarded = null;
```

## Sending Extra Data

Note that the value has to be one of the following types: `bool`, `char`, `int`, `long`, `float`, `double`, `string`.
Passing `null` as a value will remove existing `KeyValuePair` from dictionary.

```c#
bidonRewarded.SetExtraData("existing_key", null);
```

You can also read all current extras as shown below:

```c#
var extras = bidonRewarded.GetExtraData();
```

## Win / Loss Notifications

Use the following methods to let Bidon know whether or not it won the show opportunity.

```c#
bidonRewarded.NotifyWin();
```

```c#
bidonRewarded.NotifyLoss("winner_demand_id", (double)winnerEcpm);
```
