using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public abstract class SpecificationEvaluator<TEntity> where TEntity : class
    {
        // Building the final IQueryable with filter and sort
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;


            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);


            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);

                if (spec.ThenBy is not null)
                    query = ((IOrderedQueryable<TEntity>)query).ThenBy(spec.ThenBy);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);

                if (spec.ThenBy is not null)
                    query = ((IOrderedQueryable<TEntity>)query).ThenBy(spec.ThenBy);
            }


            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (query, includeExpresstion) => query.Include(includeExpresstion));

            return query;
        }
    }

}
