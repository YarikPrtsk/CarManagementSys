using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Application.Common.Customers.Commands;
using Domain.Entities;
using FluentAssertions;
using Tests.Common;
using Tests.Data;

namespace Api.Tests.Integration;

public class CustomersControllerTests : BaseIntegrationTest
{
    public CustomersControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAll_ShouldReturnCustomers_WhenCustomersExist()
    {
        // Arrange
        var customer = CustomerData.JohnDoe();
        await Context.Customers.AddAsync(customer);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync("/api/customers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customers = await response.Content.ReadFromJsonAsync<List<CustomerResponse>>();
        customers.Should().NotBeNull();
        customers.Should().Contain(c => c.Id == customer.Id && c.Email == customer.Email);
    }

    [Fact]
    public async Task Create_ShouldCreateCustomer_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateCustomerRequest(
            "Alice",
            "Wonderland",
            "alice@example.com",
            "+111222333",
            "Wonderland St.",
            new DateTime(1995, 5, 5)
        );

        // Act
        var response = await Client.PostAsJsonAsync("/api/customers", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        var customerId = result!["id"];
        
        var dbCustomer = await Context.Customers.FindAsync(customerId);
        dbCustomer.Should().NotBeNull();
        dbCustomer!.Email.Should().Be("alice@example.com");
        dbCustomer.FirstName.Should().Be("Alice");
    }
}