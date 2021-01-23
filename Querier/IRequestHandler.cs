using System.Collections.Generic;

namespace Querier
{
    public interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        IEnumerable<IResult> Handle(TRequest request, IApplicationState state);
    }
}