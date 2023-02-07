using System.ComponentModel.DataAnnotations;


namespace Trial.Core.DTO.OrderDetails;


public class UpdateOrderDetailsDTO
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Amount { get; set; }
}
