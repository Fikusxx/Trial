﻿using System.ComponentModel.DataAnnotations;

namespace Trial.Core.DTO.Product;


public class UpdateProductDTO
{
    public int Id { get; set; }

    public int ProductTypeId { get; set; }

    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int Price { get; set; }

    [Range(0, int.MaxValue)]
    public int AvailableAmount { get; set; }
}
