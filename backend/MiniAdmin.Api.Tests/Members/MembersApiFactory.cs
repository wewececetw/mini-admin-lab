using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniAdmin.Api.Auth;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Api.Tests.Members;

public sealed class MembersApiFactory : WebApplicationFactory<Program>
{
    private readonly string _dbPath =
        Path.Combine(Path.GetTempPath(), $"miniadmin-test-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var remove = services
                .Where(d =>
                    d.ServiceType == typeof(MiniAdminDbContext) ||
                    d.ServiceType == typeof(DbContextOptions<MiniAdminDbContext>))
                .ToList();
            foreach (var d in remove)
            {
                services.Remove(d);
            }

            services.AddDbContext<MiniAdminDbContext>(options =>
                options.UseSqlite($"Data Source={_dbPath}"));
        });
    }

    public void EnsureDatabase()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MiniAdminDbContext>();
        db.Database.EnsureCreated();
    }

    public Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        return CreateAuthenticatedClientAsync("admin", "secret");
    }

    public async Task<HttpClient> CreateAuthenticatedClientAsync(
        string username,
        string password)
    {
        var client = CreateClient();
        var login = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest { Username = username, Password = password });
        login.EnsureSuccessStatusCode();
        var body = await login.Content.ReadFromJsonAsync<LoginResponse>();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", body!.Token);
        return client;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing && File.Exists(_dbPath))
        {
            File.Delete(_dbPath);
        }
    }
}
