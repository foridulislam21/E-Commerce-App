using System.Linq;
using API.Abstractions.Specification;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Specification {
    public class SpecifationEvaluator<T> where T : BaseEntity {
        public static IQueryable<T> GetQuery (IQueryable<T> inputQuery, ISpecification<T> spec) {
            var query = inputQuery;
            if (spec.Criteria != null) {
                query = query.Where (spec.Criteria);
            }
            query = spec.Includes.Aggregate (query, (current, include) => current.Include (include));
            return query;
        }
    }
}