using LinqKit;
using System.Linq;
using System.Collections.Generic;
using RandomUser.Domain.ValueObjects.ListFilter;
using RandomUser.Domain.ValueObjects.ListFilter.Enum;

namespace RandomUser.Application.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Applies filters and pagination to an IEnumerable
        /// </summary>
        /// <param name="results">List to filter</param>
        /// <param name="criteria">Filters to apply</param>
        /// <returns></returns>
        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> results, FilterCriteria criteria)
        {
            var type = typeof(T);
            var predicate = PredicateBuilder.New<T>(true);

            foreach (var filter in criteria.Options)
            {
                if (filter.Name != null && filter.Value != null)
                {
                    var val = filter
                        .Value
                        .ToLowerInvariant();

                    if (filter.Name == type.Name)
                    {
                        switch (filter.Operator)
                        {
                            case FilterOperator.Equals:
                                predicate = predicate.And(p =>
                                    p.GetType()
                                    .GetProperties()
                                    .Any(a => a.PropertyType == typeof(string)
                                            && a.GetValue(p, null) != null
                                            && a.GetValue(p, null).ToString().ToLowerInvariant().Equals(val)));
                                break;
                            case FilterOperator.Contains:
                                predicate = predicate.And(p =>
                                    p.GetType()
                                    .GetProperties()
                                    .Any(a => a.PropertyType == typeof(string)
                                            && a.GetValue(p, null) != null
                                            && a.GetValue(p, null).ToString().ToLowerInvariant().Contains(val)));
                                break;
                        }
                    }
                    else if (type.GetProperty(filter.Name) != null)
                    {
                        switch (filter.Operator)
                        {
                            case FilterOperator.Equals:
                                predicate = predicate.And(p =>
                                    p.GetType()
                                    .GetProperty(filter.Name)
                                    .GetValue(p, null)
                                    .ToString()
                                    .ToLowerInvariant()
                                    .Equals(val));
                                break;
                            case FilterOperator.Contains:
                                predicate = predicate.And(p =>
                                    p.GetType()
                                    .GetProperty(filter.Name)
                                    .GetValue(p, null)
                                    .ToString()
                                    .ToLowerInvariant()
                                    .Contains(val));
                                break;
                        }
                    }

                }
            }

            results = results
                .Where(predicate)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return results;
        }
    }
}
