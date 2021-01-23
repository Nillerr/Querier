using System;
using Querier.Demo.Database;
using Querier.Notifications;
using Xunit;

namespace Querier.Testing.Demo
{
    public static class ResultExtensions
    {
        public static Action<IResult> UpdateOne<TDocument, TKey>(TKey key)
            where TKey : notnull
        {
            return result => result.ShouldBeUpdateOne<TDocument, TKey>(key);
        }

        public static void ShouldBeUpdateOne<TDocument, TKey>(this IResult source, TKey key)
            where TKey : notnull
        {
            var update = Assert.IsType<UpdateOneResult<TDocument, TKey>>(source);
            Assert.Equal(key, update.Key);
        }

        public static Action<IResult> Publish<T>(Topic<T> topic, T notification)
        {
            return result => result.ShouldBePublish(topic, notification);
        }

        public static void ShouldBePublish<T>(this IResult source, Topic<T> topic, T notification)
        {
            Assert.Equal(new PublishResult<T>(topic, notification), source);
        }

        public static Action<IResult> NotFound<TKey>(TKey key)
        {
            return result => result.ShouldBeNotFound(key);
        }

        public static void ShouldBeNotFound<TKey>(this IResult source, TKey key)
        {
            Assert.Equal(new NotFoundResult<TKey>(key), source);
        }

        public static void ShouldBeConflict<TKey>(this IResult source, TKey key)
        {
            Assert.Equal(new ConflictResult<TKey>(key), source);
        }

        public static void ShouldBeForbid(this IResult source)
        {
            Assert.Equal(new ForbidResult(), source);
        }
    }
}