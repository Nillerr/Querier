using System.Threading.Tasks;

namespace Querier
{
    public interface IValueResultHandler
    {
        Task ExecuteValueAsync<T>(T value);
    }
}