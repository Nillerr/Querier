using System.Collections.Generic;

namespace Querier.Microsoft.Extensions.Logging
{
    public static class ResultsExtensions
    {
        /// <summary>
        /// Ignores log results of type <see cref="Log{TCategoryName}"/> from the source.
        /// </summary>
        /// <param name="source">The source </param>
        /// <returns></returns>
        public static async IAsyncEnumerable<IResult> IgnoreLogging(this IAsyncEnumerable<IResult> source)
        {
            await foreach (var result in source)
            {
                var resultType = result.GetType();
                if (resultType.IsConstructedGenericType && resultType.GetGenericTypeDefinition() == typeof(Log<>))
                {
                    continue;
                }

                yield return result;
            }
        }
        
        /// <summary>
        /// Ignores log results of type <see cref="Log{TCategoryName}"/> from the source.
        /// </summary>
        /// <param name="source">The source </param>
        /// <returns></returns>
        public static IEnumerable<IResult> IgnoreLogging(this IEnumerable<IResult> source)
        {
            foreach (var result in source)
            {
                var resultType = result.GetType();
                if (resultType.IsConstructedGenericType && resultType.GetGenericTypeDefinition() == typeof(Log<>))
                {
                    continue;
                }

                yield return result;
            }
        }
    }
}