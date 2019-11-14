using System;
using System.Linq.Expressions;

namespace AssignmentAPI.Models
{
    public class SearchRequest<TEntity>
    {
        public Expression<Func<TEntity, bool>> Expression { get; set; }
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

    }
}
