using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ModificationScheduleConfiguration : IEntityTypeConfiguration<ModificationSchedule>
{
    public void Configure(EntityTypeBuilder<ModificationSchedule> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.TaskName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(ms => ms.Description)
            .HasMaxLength(1000);

        builder.Property(ms => ms.Frequency)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
    }
}