using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Demo.Database
{
    public sealed record UpdateManyResult<TDocument, TKey>(
        Func<DatabaseContext, Dictionary<TKey, TDocument>> EntitiesSelector,
        Func<TDocument, bool> Predicate,
        Func<TDocument, TDocument> Update
    ) : IResult<DatabaseContext>
        where TKey : notnull
    {
        public Task ExecuteAsync(DatabaseContext db, IResultContext context)
        {
            var entities = EntitiesSelector(db);
            foreach (var (key, entity) in entities.ToList())
            {
                entities[key] = Update(entity);
            }
            return Task.CompletedTask;
        }
    }
}