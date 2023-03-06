// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    public enum BidonError
    {
        SdkNotInitialized,

        AppKeyIsInvalid,

        InternalServerSdkError,

        NetworkError,

        AuctionInProgress,

        NoAuctionResults,

        NoRoundResults,

        NoContextFound,

        NoBid,

        NoFill,

        BidTimedOut,

        FillTimedOut,

        AdFormatIsNotSupported,

        Unspecified,

        FullscreenAdNotReady,

        NoAppropriateAdUnitId,

        Expired,
    }
}
