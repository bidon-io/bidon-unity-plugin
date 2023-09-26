// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public enum BidonError
    {
        SdkNotInitialized = 1,

        AppKeyIsInvalid,

        InternalServerSdkError,

        NetworkError,

        AuctionInProgress,

        AuctionCancelled,

        NoAuctionResults,

        NoRoundResults,

        NoContextFound,

        NoBid,

        NoFill,

        BidTimedOut,

        FillTimedOut,

        AdFormatIsNotSupported,

        Unspecified,

        AdNotReady,

        NoAppropriateAdUnitId,

        Expired,
    }
}
