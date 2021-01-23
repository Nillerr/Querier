using System.Threading.Tasks;

namespace Querier.Notifications
{
    public interface IPublisher
    {
        Task PublishAsync<T>(Topic<T> topic, T notification);
    }
}