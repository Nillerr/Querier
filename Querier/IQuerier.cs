using System.Threading.Tasks;

namespace Querier
{
    public interface IQuerier
    {
        Task InvokeHandlerAsync(IRequest request);

        Task InvokeHandlerAsync(IAsyncRequest request);
    }
}