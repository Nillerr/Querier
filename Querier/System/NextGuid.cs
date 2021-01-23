using System;

namespace Querier
{
    public sealed class NextGuid : IQuery<IGuidGenerator, Guid>
    {
        public static readonly NextGuid Instance = new();
        
        private NextGuid() { }
        
        public Guid Execute(IGuidGenerator source, IQueryContext context) => source.NewGuid();
    }
}