using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MiniAdmin.Api.Auth;
using MiniAdmin.Api.Tests.Members;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Api.Tests.Auth;

public class AuthApiTests
{
    [Fact]
    public async Task Login_valid_credentials_returns_token()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest { Username = "admin", Password = "secret" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrWhiteSpace(body.Token));
    }

    [Fact]
    public async Task Login_invalid_credentials_returns_401()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest { Username = "admin", Password = "wrong" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Members_without_token_returns_401()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/members");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Delete_as_user_returns_403()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
        }

        var client = await factory.CreateAuthenticatedClientAsync("user", "secret");
        var response = await client.DeleteAsync("/api/members/1");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
