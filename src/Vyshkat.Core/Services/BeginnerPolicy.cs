using Vyshkat.Core.Domain;

namespace Vyshkat.Core.Services;

public sealed class BeginnerPolicy
{
    public BeginnerPolicy(int minimumScore = 6)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(minimumScore, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(minimumScore, 10);
        MinimumScore = minimumScore;
    }

    public int MinimumScore { get; }

    public bool IsSuitable(Transport transport) => transport.BeginnerScore >= MinimumScore;
}
