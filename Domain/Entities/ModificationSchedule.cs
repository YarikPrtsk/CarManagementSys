using Domain.Enums;

namespace Domain.Entities;

public class ModificationSchedule
{
    public Guid Id { get; private set; }
    public Guid CarId { get; private set; }
    public string TaskName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ModificationFrequency Frequency { get; private set; }
    public DateTime NextDueDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private ModificationSchedule() { }

    public static ModificationSchedule New(
        Guid id,
        Guid carId,
        string taskName,
        string description,
        ModificationFrequency frequency,
        DateTime nextDueDate)
    {
        return new ModificationSchedule
        {
            Id = id,
            CarId = carId,
            TaskName = taskName,
            Description = description,
            Frequency = frequency,
            NextDueDate = nextDueDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateSchedule(string taskName, string description, ModificationFrequency frequency, DateTime nextDueDate)
    {
        TaskName = taskName;
        Description = description;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
