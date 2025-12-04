using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Application.Common.CarSales.Commands;
using Domain.Enums;
using FluentAssertions;
using Tests.Common;
using Tests.Data;

namespace Api.Tests.Integration;

public class CarSalesControllerTests : BaseIntegrationTest
{
    public CarSalesControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldCreateSale_WhenCarExists()
    {
        // Arrange
        var car = CarData.ToyotaCamry();
        await Context.Cars.AddAsync(car);
        await Context.SaveChangesAsync();

        var request = new CreateCarSaleRequest(
            car.Id,
            null,
            "SALE-001",
            "Guest User",
            "N/A",
            "Sale of Toyota",
            "Description",
            SalePriority.Low,
            DateTime.UtcNow.AddDays(5)
        );

        // Act
        var response = await Client.PostAsJsonAsync("/api/car-sales", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        var saleId = result!["id"];
        
        var saleInDb = await Context.CarSales.FindAsync(saleId);
        saleInDb.Should().NotBeNull();
        saleInDb!.SaleNumber.Should().Be("SALE-001");
        saleInDb.CarId.Should().Be(car.Id);
    }

    [Fact]
    public async Task Start_ShouldChangeStatusToInProgress()
    {
        // Arrange
        var car = CarData.HondaCivic();
        await Context.Cars.AddAsync(car);
        var sale = CarSaleData.StandardSale(car.Id);
        await Context.CarSales.AddAsync(sale);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.PatchAsync($"/api/car-sales/{sale.Id}/start", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        await Context.Entry(sale).ReloadAsync();
        sale.Status.Should().Be(SaleStatus.InProgress);
    }
}