using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity>
    {
        // Filter condition
        Expression<Func<TEntity, bool>> Criteria { get; set; }

        // Sorting newest to oldest
        Expression<Func<TEntity, object>> OrderByDescending { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsPagination { get; set; }
    }
}
