using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MiniAdmin.Api.Tests.Members;
using MiniAdmin.Core.Products;

namespace MiniAdmin.Api.Tests.Products;

public class ProductsApiTests
{
    [Fact]
    public async Task GetAll_returns_empty_list_when_no_products()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Empty(products);
    }

    [Fact]
    public async Task GetAll_returns_seeded_products()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            await repo.AddAsync(new Product(1, "筆記本", 100m));
            await repo.AddAsync(new Product(2, "原子筆", 20m));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Equal(2, products.Count);
        Assert.Equal(1, products[0].Id);
        Assert.Equal("筆記本", products[0].Name);
        Assert.Equal(100m, products[0].Price);
        Assert.Equal(2, products[1].Id);
        Assert.Equal("原子筆", products[1].Name);
        Assert.Equal(20m, products[1].Price);
    }

    [Fact]
    public async Task Create_returns_201_and_body()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.PostAsJsonAsync(
            "/api/products",
            new Product(3, "橡皮擦", 15m));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(3, product.Id);
        Assert.Equal("橡皮擦", product.Name);
        Assert.Equal(15m, product.Price);
    }

    [Fact]
    public async Task Create_persists_product_for_GetAll()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var create = await client.PostAsJsonAsync(
            "/api/products",
            new Product(3, "橡皮擦", 15m));
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);

        var get = await client.GetAsync("/api/products");
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);

        var products = await get.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Single(products);
        Assert.Equal(3, products[0].Id);
        Assert.Equal("橡皮擦", products[0].Name);
        Assert.Equal(15m, products[0].Price);
    }

    [Fact]
    public async Task Delete_returns_204()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            await repo.AddAsync(new Product(1, "筆記本", 100m));
            await repo.AddAsync(new Product(2, "原子筆", 20m));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.DeleteAsync("/api/products/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var get = await client.GetAsync("/api/products");
        var products = await get.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Single(products);
        Assert.Equal(2, products[0].Id);
    }

    [Fact]
    public async Task Delete_returns_404_when_missing()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.DeleteAsync("/api/products/99");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_returns_200_and_body()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();

        using (var scope = factory.Services.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            await repo.AddAsync(new Product(1, "筆記本", 100m));
        }

        var client = await factory.CreateAuthenticatedClientAsync();
        var response = await client.PutAsJsonAsync(
            "/api/products/1",
            new Product(1, "精裝筆記本", 150m));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("精裝筆記本", product.Name);
        Assert.Equal(150m, product.Price);
    }

    [Fact]
    public async Task Update_returns_404_when_missing()
    {
        await using var factory = new MembersApiFactory();
        factory.EnsureDatabase();
        var client = await factory.CreateAuthenticatedClientAsync();

        var response = await client.PutAsJsonAsync(
            "/api/products/99",
            new Product(99, "幽靈", 1m));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
