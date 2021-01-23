using System.Threading.Tasks;

namespace Querier
{
    public interface IAsyncQuery<in TSource, TResult>
    {
        Task<TResult> ExecuteAsync(TSource source, IQueryContext context);
    }
}