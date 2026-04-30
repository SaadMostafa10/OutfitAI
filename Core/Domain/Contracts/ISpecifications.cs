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
        List<Expression<Func<TEntity, object>>> Includes { get; set; }
        Expression<Func<TEntity, bool>>? Criteria { get; set; }


        // Sorting newest to oldest
        Expression<Func<TEntity, Object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        Expression<Func<TEntity, object>>? ThenBy { get; set; }

        int Skip { get; set; }
        int Take { get; set; }
        bool IsPagination { get; set; }
    }
}
