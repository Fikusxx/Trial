

namespace Trial.Core.Common.Filtering;

public class OrderFilter
{
    public int CustomerId { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
}
