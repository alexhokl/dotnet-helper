using System;
using System.Collections.Generic;
using System.Linq;

namespace Alexhokl.Helpers
{
    public static class PagingHelper
    {
        /// <summary>
        /// Returns an enumerable of items of the specified page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Page size must be a positive integer.
        /// or
        /// Page must be a non-negative integer.
        /// </exception>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> query, int pageSize, int page)
        {
            if (pageSize <= 0)
                throw new ArgumentException("Page size must be a positive integer.");
            if (page < 0)
                throw new ArgumentException("Page must be a non-negative integer.");

            int itemsToSkip = pageSize * page;
            return
                query
                    .Skip(itemsToSkip)
                    .Take(pageSize);
        }
    }
}
