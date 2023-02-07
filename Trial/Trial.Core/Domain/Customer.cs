using System.ComponentModel.DataAnnotations;


namespace Trial.Core.Domain;


public class Customer
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }   

    public IEnumerable<Order>? Orders { get; set; }
}
