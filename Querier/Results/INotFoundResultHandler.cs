using System.Threading.Tasks;

namespace Querier
{
    public interface INotFoundResultHandler
    {
        Task ExecuteNotFoundAsync<TKey>(TKey key);
    }
}