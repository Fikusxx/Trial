using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trial.Core.Common.Enums;
using Trial.Core.Domain;


namespace Trial.Persistence.Configurations;


public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        var converter = new EnumToStringConverter<ProductTypes>();

        builder.Property(x => x.Type).HasConversion(converter);

        builder.HasData(
            new ProductType() { Id = 1, Type = ProductTypes.Product },
            new ProductType() { Id = 2, Type = ProductTypes.Service },
            new ProductType() { Id = 3, Type = ProductTypes.Other }
            );
    }
}
