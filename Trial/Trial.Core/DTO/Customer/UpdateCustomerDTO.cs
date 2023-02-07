using System.ComponentModel.DataAnnotations;


namespace Trial.Core.DTO.Customer;


public class UpdateCustomerDTO
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }
}
