using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Querier.Testing
{
    public static class AsyncEnumerableExtensions
    {
        public static TaskAwaiter<List<IResult>> GetAwaiter(this IAsyncEnumerable<IResult> source)
        {
            return source.ToListAsync().GetAwaiter();
        }
        
        public static async Task<List<IResult>> ToListAsync(this IAsyncEnumerable<IResult> source)
        {
            var results = new List<IResult>();
            
            await foreach (var result in source)
            {
                results.Add(result);
            }

            return results;
        }
    }
}