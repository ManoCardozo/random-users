using System.Collections.Generic;

namespace RandomUser.Domain.ValueObjects.ListFilter
{
    public class FilterCriteria
    {
        public FilterCriteria()
        {
            Page = 1;
            PageSize = 20;
            Options = new List<Filter>();
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<Filter> Options { get; set; }
    }
}
