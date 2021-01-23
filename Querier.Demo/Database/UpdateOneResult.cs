using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Querier.Demo.Database
{
    public sealed record UpdateOneResult<TDocument, TKey>(
        Func<DatabaseContext, Dictionary<TKey, TDocument>> EntitiesSelector,
        TKey Key,
        Func<TDocument, TDocument> Update
    ) : IResult<DatabaseContext>
        where TKey : notnull
    {
        public Task ExecuteAsync(DatabaseContext db, IResultContext context)
        {
            var entities = EntitiesSelector(db);
            entities[Key] = Update(entities[Key]);
            return Task.CompletedTask;
        }
    }
}