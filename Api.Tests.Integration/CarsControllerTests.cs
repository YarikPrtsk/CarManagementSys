using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Application.Common.Cars.Commands;
using Domain.Entities;
using FluentAssertions;
using Tests.Data;
using Tests.Common;
using Xunit.Abstractions;

namespace Api.Tests.Integration;

public class CarsControllerTests : BaseIntegrationTest
{
    public CarsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAll_ShouldReturnCars_WhenCarsExist()
    {
        // Arrange
        var car = CarData.ToyotaCamry();
        await Context.Cars.AddAsync(car);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync("/api/cars");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var cars = await response.Content.ReadFromJsonAsync<List<CarResponse>>();
        cars.Should().NotBeNull();
        cars.Should().Contain(c => c.Id == car.Id && c.Make == car.Make);
    }

    [Fact]
    public async Task GetById_ShouldReturnCar_WhenCarExists()
    {
        // Arrange
        var car = CarData.HondaCivic();
        await Context.Cars.AddAsync(car);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/cars/{car.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var returnedCar = await response.Content.ReadFromJsonAsync<CarResponse>();
        returnedCar.Should().NotBeNull();
        returnedCar!.Id.Should().Be(car.Id);
        returnedCar.Make.Should().Be(car.Make);
        returnedCar.Model.Should().Be(car.Model);
    }

    [Fact]
    public async Task Create_ShouldCreateCar_WhenRequestIsValid()
    {
        // Arrange
        var car = CarData.TeslaModel3();
        var request = new CreateCarRequest(
            car.Make,
            car.Model,
            car.VinNumber,
            car.Color,
            car.Price,
            car.Year
        );

        // Act
        var response = await Client.PostAsJsonAsync("/api/cars", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        result.Should().NotBeNull();
        var carId = result!["id"];
    
        var carInDb = await Context.Cars.FindAsync(carId);
        carInDb.Should().NotBeNull();
        carInDb!.Make.Should().Be(car.Make);
        carInDb.Model.Should().Be(car.Model);
        carInDb.VinNumber.Should().Be(car.VinNumber);
        carInDb.Color.Should().Be(car.Color);
        carInDb.Price.Should().Be(car.Price);
    }

    [Fact]
    public async Task Update_ShouldUpdateCar_WhenCarExists()
    {
        // Arrange
        var car = CarData.TeslaModel3();
        await Context.Cars.AddAsync(car);
        await Context.SaveChangesAsync();

        var request = new UpdateCarRequest("Tesla", "Model 3 Performance", "Matte Black", 60000m);

        // Act
        var response = await Client.PutAsJsonAsync($"/api/cars/{car.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        await Context.Entry(car).ReloadAsync();
        car.Model.Should().Be("Model 3 Performance");
        car.Color.Should().Be("Matte Black");
        car.Price.Should().Be(60000m);
    }
}