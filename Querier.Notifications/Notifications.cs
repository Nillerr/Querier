namespace Querier.Notifications
{
    public static class Notifications
    {
        public static PublishResult<T> Publish<T>(Topic<T> topic, T notification) => new(topic, notification);
    }
}