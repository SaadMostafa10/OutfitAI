using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity>
    {
        // Constructor that takes the filter criteria
        public BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        public Expression<Func<TEntity, object>>? ThenBy { get; set; }


        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; } 


        // Method to set sorting in our specific specs
        public void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
        {
            OrderBy = orderBy;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>>? orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        public void AddThenBy(Expression<Func<TEntity, object>>? thenBy)
        {
            ThenBy = thenBy;
        }


        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;


        }
    }

}
