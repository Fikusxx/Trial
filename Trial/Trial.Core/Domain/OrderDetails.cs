using System.ComponentModel.DataAnnotations;


namespace Trial.Core.Domain;


public class OrderDetails
{
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int Position { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int TotalPrice { get; set; } 

    public int Amount { get; set; }
}
