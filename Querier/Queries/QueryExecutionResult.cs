using System.Diagnostics.CodeAnalysis;

namespace Querier
{
    public readonly struct QueryExecutionResult<T>
    {
        public QueryExecutionResultType Type { get; }
        
        [AllowNull]
        public T Result { get; }

        public QueryExecutionResult([AllowNull] T result)
        {
            Type = QueryExecutionResultType.Success;
            Result = result;
        }

        public static implicit operator QueryExecutionResult<T>(T success) => new(success);
    }
}