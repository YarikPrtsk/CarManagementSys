using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CarSaleConfiguration : IEntityTypeConfiguration<CarSale>
{
    public void Configure(EntityTypeBuilder<CarSale> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SaleNumber)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasIndex(s => s.SaleNumber)
            .IsUnique();

        builder.Property(s => s.CustomerName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.CustomerContact)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(1000);

        builder.Property(s => s.CompletionNotes)
            .HasMaxLength(1000);

        builder.Property(s => s.Priority)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
    }
}