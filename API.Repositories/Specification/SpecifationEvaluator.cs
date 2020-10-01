using System.Linq;
using API.Abstractions.Specification;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Specification
{
    public class SpecifationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery (IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where (spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy (spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending (spec.OrderByDescending);
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip (spec.Skip).Take (spec.Take);
            }
            query = spec.Includes.Aggregate (query, (current, include) => current.Include (include));
            return query;
        }
    }
}