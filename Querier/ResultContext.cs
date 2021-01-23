using System;

namespace Querier
{
    public sealed class ResultContext : IResultContext
    {
        public ResultContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}