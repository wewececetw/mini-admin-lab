using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Api.Tests.Members;

public class MembersApiTests
{
    [Fact]
    public void DI_uses_EfMemberRepository()
    {
        using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using var scope = factory.Services.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();

        Assert.IsType<EfMemberRepository>(repo);
    }

    [Fact]
    public async Task GetAll_returns_seeded_members()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
            await repo.AddAsync(new Member(2, "Bob", "bob@example.com"));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.GetAsync("/api/members");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var members = await response.Content.ReadFromJsonAsync<List<Member>>();
        Assert.NotNull(members);
        Assert.Equal(2, members.Count);
        Assert.Equal(1, members[0].Id);
        Assert.Equal("Ada", members[0].Name);
        Assert.Equal("ada@example.com", members[0].Email);
        Assert.Equal(2, members[1].Id);
        Assert.Equal("Bob", members[1].Name);
        Assert.Equal("bob@example.com", members[1].Email);
    }

    [Fact]
    public async Task GetAll_returns_empty_list_when_no_members()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/members");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var members = await response.Content.ReadFromJsonAsync<List<Member>>();
        Assert.NotNull(members);
        Assert.Empty(members);
    }

    [Fact]
    public async Task GetById_returns_the_member()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
            await repo.AddAsync(new Member(2, "Bob", "bob@example.com"));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.GetAsync("/api/members/2");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var member = await response.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(member);
        Assert.Equal(2, member.Id);
        Assert.Equal("Bob", member.Name);
        Assert.Equal("bob@example.com", member.Email);
    }

    [Fact]
    public async Task GetById_returns_404_when_missing()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/members/99");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_returns_201_and_body()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.PostAsJsonAsync(
            "/api/members",
            new Member(3, "Cara", "cara@example.com"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var member = await response.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(member);
        Assert.Equal(3, member.Id);
        Assert.Equal("Cara", member.Name);
        Assert.Equal("cara@example.com", member.Email);
    }

    [Fact]
    public async Task Create_persists_member_for_GetById()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var create = await client.PostAsJsonAsync(
            "/api/members",
            new Member(3, "Cara", "cara@example.com"));
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);

        var get = await client.GetAsync("/api/members/3");
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);

        var member = await get.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(member);
        Assert.Equal(3, member.Id);
        Assert.Equal("Cara", member.Name);
        Assert.Equal("cara@example.com", member.Email);
    }

    [Fact]
    public async Task Update_returns_200_and_updated_body()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.PutAsJsonAsync(
            "/api/members/1",
            new Member(1, "Ada Lovelace", "ada.lovelace@example.com"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var member = await response.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(member);
        Assert.Equal(1, member.Id);
        Assert.Equal("Ada Lovelace", member.Name);
        Assert.Equal("ada.lovelace@example.com", member.Email);
    }

    [Fact]
    public async Task Update_returns_404_when_missing()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.PutAsJsonAsync(
            "/api/members/99",
            new Member(99, "Ghost", "ghost@example.com"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_returns_204()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
            await repo.AddAsync(new Member(2, "Bob", "bob@example.com"));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.DeleteAsync("/api/members/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getDeleted = await client.GetAsync("/api/members/1");
        Assert.Equal(HttpStatusCode.NotFound, getDeleted.StatusCode);

        var getBob = await client.GetAsync("/api/members/2");
        Assert.Equal(HttpStatusCode.OK, getBob.StatusCode);
    }

    [Fact]
    public async Task Delete_returns_404_when_missing()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.DeleteAsync("/api/members/99");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
