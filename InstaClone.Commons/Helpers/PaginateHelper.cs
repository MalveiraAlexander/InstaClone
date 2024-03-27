using InstaClone.Commons.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Helpers
{
    public static class PaginateHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, SortedBaseFilter paginateOption)
        {
            if (paginateOption == null) return query;
            if (!paginateOption.Page.HasValue) paginateOption.Page = 1;
            if (!paginateOption.PageSize.HasValue) paginateOption.PageSize = 10;
            if (!string.IsNullOrEmpty(paginateOption.Order) && !string.IsNullOrWhiteSpace(paginateOption.Order))
                query = query.OrderBy(paginateOption.Order);

            query = query.Skip((paginateOption.Page.Value - 1) * paginateOption.PageSize.Value)
                         .Take(paginateOption.PageSize.Value);
            return query;
        }
    }
}
