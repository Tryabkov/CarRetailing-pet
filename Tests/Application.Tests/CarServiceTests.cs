

using System.Net.Http.Json;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace Application.Tests;

public class CarE2ETests : IClassFixture<FactoryFixture>
{
    private readonly FactoryFixture _fixture;
    private readonly ITestOutputHelper _output;
    private HttpClient _client => _fixture.Client;

    public CarE2ETests(FactoryFixture fixture,  ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }


    [Fact]
    public async Task Crud_WithAuth_ShouldWork()
    {
        // Registration
        var registerResponse = await _client.PostAsJsonAsync("/auth/signup", new
        {
            Username = "123",
            Email = "1@1",
            Password = "1"
        });
        var body = await registerResponse.Content.ReadAsStringAsync();
        Assert.True(registerResponse.IsSuccessStatusCode, $"Signup failed: {registerResponse.StatusCode}, {body}");
        registerResponse.EnsureSuccessStatusCode();
        
        // Login
        var loginResponse = await _client.PostAsJsonAsync("/auth/login", new
        {
            Email = "1@1",
            Password = "1"
        });
        loginResponse.EnsureSuccessStatusCode();
        
        // Create Car
        var createResponse = await _client.PostAsJsonAsync("/api/cars", new
        {
            Mark = "Porch",
            Model = "911",
            Price = 100000,
            Year = 2020,
            Mileage = 100000,
            Description = "desc desc desc desc desc desc desc desc desc desc desc"
        });
        createResponse.EnsureSuccessStatusCode();

        var content = await createResponse.Content.ReadAsStringAsync();
        if (!uint.TryParse(content, out var carId))
        {
            Assert.Fail();
        }
        
        // Get car by id
        var getIdResponse = await _client.GetAsync($"/api/cars/{carId}");
        getIdResponse.EnsureSuccessStatusCode();
        var car = await getIdResponse.Content.ReadFromJsonAsync<ReturnCarDto>();
        Assert.NotNull(car);
        Assert.Equal("911", car.Model);
        
        // Update car
        var updateResponse = await _client.PatchAsJsonAsync($"/api/cars/{carId}", new
        {
            Model = "Macan"
        });
        // updateResponse.EnsureSuccessStatusCode();
        
        // Get car by query 
        var getQueryResponse = await _client.GetAsync($"/api/cars/all?Mark=Porch");
        getQueryResponse.EnsureSuccessStatusCode();
        var cars = await getQueryResponse.Content.ReadFromJsonAsync<List<ReturnCarDto>>();
        Assert.NotNull(cars);
        car = cars.FirstOrDefault();
        Assert.Equal("Porch", car.Mark);
        

    }
}