using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Testing
{
    public class TestApplicationState : IApplicationState, IDisposable
    {
        #nullable disable
        private readonly Dictionary<object, object> _queries = new();
        #nullable enable
        
        public void Expect<TSource, TResult>(IQuery<TSource, TResult> query, TResult result)
        {
            _queries[query] = result;
        }
        
        public TResult Query<TSource, TResult>(IQuery<TSource, TResult> query)
        {
            if (_queries.Remove(query, out var result))
            {
                return (TResult) result;
            }

            throw new InvalidOperationException($"Missing expectation for the query {query}");
        }
        
        public void Expect<TSource, TResult>(IAsyncQuery<TSource, TResult> query, TResult result)
        {
            _queries[query] = result;
        }

        public Task<TResult> QueryAsync<TSource, TResult>(IAsyncQuery<TSource, TResult> query)
        {
            if (_queries.Remove(query, out var result))
            {
                return (Task<TResult>) result;
            }

            throw new InvalidOperationException($"Missing expectation for the query {query}");
        }

        public void Dispose()
        {
            if (_queries.Count > 0)
            {
                var queries = string.Join(Environment.NewLine, _queries.Select(kvp => $"\t{kvp.Key}"));
                throw new InvalidOperationException($"The following queries were expected but not called:{Environment.NewLine}{queries}{Environment.NewLine}");
            }
        }
    }
}