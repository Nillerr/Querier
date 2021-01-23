using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Querier
{
    public readonly struct AsyncHandlerInvocationResult
    {
        public HandlerInvocationResultType Type { get; }
        
        [AllowNull]
        public IAsyncEnumerable<IResult> Result { get; }

        public AsyncHandlerInvocationResult([AllowNull] IAsyncEnumerable<IResult> result)
        {
            Type = HandlerInvocationResultType.Success;
            Result = result;
        }
    }
}