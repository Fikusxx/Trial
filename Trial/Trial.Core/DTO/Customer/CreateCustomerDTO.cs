using System.ComponentModel.DataAnnotations;

namespace Trial.Core.DTO.Customer;

public class CreateCustomerDTO
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }
}
