using mego.Core.Models;
using mego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mego.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Ordering<T>(this IQueryable<T> query, IQueryObject queryObject,
             Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if ((string.IsNullOrWhiteSpace(queryObject.SortBy))
                || (!columnsMap.ContainsKey(queryObject.SortBy)))
                return query;

            if (queryObject.IsSort)
                return query = query.OrderBy(columnsMap[queryObject.SortBy]);

            else
                return query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.Page <= 0)
                queryObject.Page = 1;

            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 5;

            return query = query.Skip((queryObject.Page - 1) * queryObject.PageSize)
                 .Take(queryObject.PageSize);
        }
    }
}
