// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

        IncorrectAdUnit,
    }
}
