using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Querier.Demo.Database
{
    public sealed record InsertOneResult<TDocument, TKey>(
        Func<DatabaseContext, Dictionary<TKey, TDocument>> EntitiesSelector,
        TKey Key,
        TDocument Document
    ) : IResult<DatabaseContext>
        where TKey : notnull
    {
        public Task ExecuteAsync(DatabaseContext db, IResultContext context)
        {
            var entities = EntitiesSelector(db);
            entities.Add(Key, Document);
            return Task.CompletedTask;
        }
    }
}