using System;

namespace Querier
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }
}