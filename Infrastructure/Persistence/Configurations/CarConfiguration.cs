using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Make)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.VinNumber)
            .HasMaxLength(50) // VIN зазвичай 17 символів, але дамо запас
            .IsRequired();

        builder.HasIndex(c => c.VinNumber)
            .IsUnique(); // VIN має бути унікальним

        builder.Property(c => c.Color)
            .HasMaxLength(50);

        builder.Property(c => c.Price)
            .HasColumnType("decimal(18,2)"); // Стандарт для грошей

        builder.Property(c => c.Status)
            .HasConversion<string>() // Зберігаємо Enum як рядок
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany<CarSale>() // Якщо у Car є навігаційна властивість Sales
            .WithOne()             // Якщо у CarSale є навігаційна властивість Car
            .HasForeignKey(s => s.CarId)
            .OnDelete(DeleteBehavior.Restrict); // Не видаляти авто, якщо є продажі
    }
}