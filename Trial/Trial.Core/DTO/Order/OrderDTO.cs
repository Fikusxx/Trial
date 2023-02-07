

namespace Trial.Core.DTO.Order;

public class OrderDTO
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
