using RandomUser.Domain.ValueObjects.ListFilter.Enum;

namespace RandomUser.Domain.ValueObjects.ListFilter
{
    public class Filter
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public FilterOperator Operator { get; set; }
    }
}
