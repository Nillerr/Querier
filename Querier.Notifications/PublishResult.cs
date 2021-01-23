using System.Threading.Tasks;

namespace Querier.Notifications
{
    public sealed record PublishResult<T>(Topic<T> Topic, T Notification) : IResult<IPublisher>
    {
        public Task ExecuteAsync(IPublisher publisher, IResultContext context)
        {
            return publisher.PublishAsync(Topic, Notification);
        }
    }
}