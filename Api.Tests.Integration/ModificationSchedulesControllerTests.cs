using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Application.Common.ModificationSchedules.Commands;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Tests.Common;
using Tests.Data;

namespace Api.Tests.Integration;

public class ModificationSchedulesControllerTests : BaseIntegrationTest
{
    public ModificationSchedulesControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetByCarId_ShouldReturnSchedules_WhenSchedulesExist()
    {
        // Arrange
        var car = CarData.ToyotaCamry();
        await Context.Cars.AddAsync(car);
        var schedule = ModificationScheduleData.OilChange(car.Id);
        await Context.ModificationSchedules.AddAsync(schedule);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/modification-schedules/car/{car.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var schedules = await response.Content.ReadFromJsonAsync<List<ModificationScheduleResponse>>();
        schedules.Should().NotBeNull();
        schedules.Should().Contain(s => s.Id == schedule.Id);
    }

    [Fact]
    public async Task Create_ShouldAddSchedule_WhenCarExists()
    {
        // Arrange
        var car = CarData.TeslaModel3();
        await Context.Cars.AddAsync(car);
        await Context.SaveChangesAsync();

        var request = new CreateModificationScheduleRequest(
            car.Id,
            "Battery Check",
            "Annual battery health check",
            ModificationFrequency.DailyCleaning,
            DateTime.UtcNow.AddYears(1)
        );

        // Act
        var response = await Client.PostAsJsonAsync("/api/modification-schedules", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        var scheduleId = result!["id"];
        
        var schedule = await Context.ModificationSchedules.FindAsync(scheduleId);
        schedule.Should().NotBeNull();
        schedule!.TaskName.Should().Be("Battery Check");
        schedule.CarId.Should().Be(car.Id);
    }
}