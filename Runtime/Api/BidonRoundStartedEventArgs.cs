using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BidonRoundStartedEventArgs : EventArgs
    {
        public string RoundId { get; }

        public double PriceFloor { get; }

        public BidonRoundStartedEventArgs(string roundId, double priceFloor)
        {
            RoundId = roundId;
            PriceFloor = priceFloor;
        }
    }
}
