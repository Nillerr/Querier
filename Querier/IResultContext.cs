using System;

namespace Querier
{
    public interface IResultContext
    {
        IServiceProvider ServiceProvider { get; }
    }
}