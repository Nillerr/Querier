using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Querier
{
    public readonly struct HandlerInvocationResult
    {
        public HandlerInvocationResultType Type { get; }
        
        [AllowNull]
        public IEnumerable<IResult> Result { get; }

        public HandlerInvocationResult([AllowNull] IEnumerable<IResult> result)
        {
            Type = HandlerInvocationResultType.Success;
            Result = result;
        }
    }
}