using Domain.Entities;
using Domain.Enums;

namespace Tests.Data;

public static class ModificationScheduleData
{
    public static ModificationSchedule OilChange(Guid carId) => ModificationSchedule.New(
        Guid.NewGuid(),
        carId,
        "Oil Change",
        "Regular oil change service",
        ModificationFrequency.MonthlyDetailing,
        DateTime.UtcNow.AddMonths(6)
    );

    public static ModificationSchedule TireRotation(Guid carId) => ModificationSchedule.New(
        Guid.NewGuid(),
        carId,
        "Tire Rotation",
        "Rotate tires every year",
        ModificationFrequency.QuarterlyService,
        DateTime.UtcNow.AddYears(1)
    );
}