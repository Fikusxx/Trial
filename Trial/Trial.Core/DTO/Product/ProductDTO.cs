

namespace Trial.Core.DTO.Product;


public class ProductDTO
{
    public int Id { get; set; }

    public int ProductTypeId { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public int AvailableAmount { get; set; }
}
