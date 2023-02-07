
namespace Trial.Core.Domain;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public IEnumerable<OrderDetails>? OrderDetails { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
