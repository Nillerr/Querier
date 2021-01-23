using System;

namespace Querier
{
    public sealed class GuidGenerator : IGuidGenerator
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}