using System;

namespace Querier
{
    public interface IQueryContext
    {
        IServiceProvider ServiceProvider { get; }
    }
}