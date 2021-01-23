using System.Threading.Tasks;

namespace Querier
{
    public interface IForbidResultHandler
    {
        Task ExecuteForbidAsync();
    }
}