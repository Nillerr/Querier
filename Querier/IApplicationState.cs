using System.Threading.Tasks;

namespace Querier
{
    public interface IApplicationState
    {
        TResult Query<TSource, TResult>(IQuery<TSource, TResult> query);
        
        Task<TResult> QueryAsync<TSource, TResult>(IAsyncQuery<TSource, TResult> query);
    }
}