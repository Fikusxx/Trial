using Trial.Core.Common.Enums;


namespace Trial.Core.Common.Filtering;


public class ProductFilter
{
    public ProductTypes ProductType { get; set; } = ProductTypes.Product;
    public bool IsAvailable { get; set; } = true;
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = int.MaxValue;
}
