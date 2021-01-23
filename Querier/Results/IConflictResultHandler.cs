using System.Threading.Tasks;

namespace Querier
{
    public interface IConflictResultHandler
    {
        Task ExecuteConflictAsync<TKey>(TKey key);
    }
}