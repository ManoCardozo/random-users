using System.Web;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using RandomUser.Domain.ValueObjects.ListFilter;
using RandomUser.Domain.ValueObjects.ListFilter.Enum;

namespace RandomUser.WebUI.Filters
{
    public class QueryStringAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            BuildFilterCriteria(context);

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Builds the filter criteria parameter base on a list of column names and column values specified on a query string.
        /// This excludes page and page size parameters.
        /// </summary>
        private static void BuildFilterCriteria(ActionExecutingContext context)
        {
            var filterCriteriaArg = context
                            .ActionArguments
                            .FirstOrDefault(a => a.Value is FilterCriteria);

            var qsFilters = HttpUtility
                .ParseQueryString(context.HttpContext.Request.QueryString.Value);

            var filters = qsFilters
                .AllKeys
                .SelectMany(qsFilters.GetValues, (k, v) => new { key = k, value = v });

            var options = new List<Filter>();
            foreach (var filter in filters)
            {
                if (filter.key != nameof(FilterCriteria.Page) && filter.key != nameof(FilterCriteria.PageSize))
                {
                    options.Add(new Filter
                    {
                        Name = filter.key,
                        Value = filter.value,
                        Operator = FilterOperator.Contains
                    });
                }
            }

            if (options.Any())
            {
                (filterCriteriaArg.Value as FilterCriteria).Options = options;
            }
        }
    }
}
