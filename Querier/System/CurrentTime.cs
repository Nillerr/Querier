using System;

namespace Querier
{
    public sealed class CurrentTime : IQuery<ISystemClock, DateTimeOffset>
    {
        public static readonly CurrentTime Instance = new();
        
        private CurrentTime() { }
        
        public DateTimeOffset Execute(ISystemClock source, IQueryContext context)
        {
            return source.UtcNow;
        }
    }
}