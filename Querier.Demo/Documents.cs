using System;
using System.Collections.Generic;
using Querier.Demo.Database;

namespace Querier.Demo
{
    public static class Documents
    {
        public static InsertOneResult<TDocument, TKey> InsertOne<TDocument, TKey>(
            Func<DatabaseContext, Dictionary<TKey, TDocument>> entitiesSelector,
            TKey key,
            TDocument document
        )
            where TKey : notnull
        {
            return new(entitiesSelector, key, document);
        }
        
        public static UpdateOneResult<TDocument, TKey> UpdateOne<TDocument, TKey>(
            Func<DatabaseContext, Dictionary<TKey, TDocument>> entitiesSelector,
            TKey key,
            Func<TDocument, TDocument> update
        )
            where TKey : notnull
        {
            return new(entitiesSelector, key, update);
        }
        
        public static UpdateManyResult<TDocument, TKey> UpdateMany<TDocument, TKey>(
            Func<DatabaseContext, Dictionary<TKey, TDocument>> entitiesSelector,
            Func<TDocument, bool> predicate,
            Func<TDocument, TDocument> update
        )
            where TKey : notnull
        {
            return new(entitiesSelector, predicate, update);
        }
    }
}