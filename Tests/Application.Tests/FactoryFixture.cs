using System.Net;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApi;

namespace Application.Tests;

public class FactoryFixture : IAsyncLifetime
{
    private CookieContainer _cookieContainer;
    public PostgreSqlContainer Postgres { get; private set; }
    public WebApplicationFactory<Program> Factory { get; private set; }
    public HttpClient Client { get; private set; }

    public async Task InitializeAsync()
    {
        Postgres = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("testpass")
            .Build();

        await Postgres.StartAsync();

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.UseUrls("https://localhost:5001");
                builder.ConfigureServices(services =>
                {
                    var connString = Postgres.GetConnectionString();

                    services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connString));

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                });
            });

        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    public async Task DisposeAsync()
    {
        Client?.Dispose();
        Factory?.Dispose();
        if (Postgres != null)
            await Postgres.StopAsync();
    }
}