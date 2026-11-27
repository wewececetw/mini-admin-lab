using MiniAdmin.Core.Products;

namespace MiniAdmin.Core.Tests.Products;

public class ProductRepositoryTests
{
    [Fact]
    public async Task AllAsync_empty_when_nothing_added()
    {
        IProductRepository repo = new MemoryProductRepository();

        var list = await repo.AllAsync();

        Assert.Empty(list);
    }

    [Fact]
    public async Task AddAsync_then_AllAsync_returns_products()
    {
        IProductRepository repo = new MemoryProductRepository();
        await repo.AddAsync(new Product(1, "筆記本", 100m));
        await repo.AddAsync(new Product(2, "原子筆", 20m));

        var list = await repo.AllAsync();

        Assert.Equal(2, list.Count);
        Assert.Equal(1, list[0].Id);
        Assert.Equal("筆記本", list[0].Name);
        Assert.Equal(100m, list[0].Price);
        Assert.Equal(2, list[1].Id);
        Assert.Equal("原子筆", list[1].Name);
        Assert.Equal(20m, list[1].Price);
    }

    [Fact]
    public async Task DeleteAsync_removes_matching_product()
    {
        IProductRepository repo = new MemoryProductRepository();
        await repo.AddAsync(new Product(1, "筆記本", 100m));
        await repo.AddAsync(new Product(2, "原子筆", 20m));

        await repo.DeleteAsync(1);

        var list = await repo.AllAsync();
        Assert.Single(list);
        Assert.Equal(2, list[0].Id);
    }

    [Fact]
    public async Task DeleteAsync_throws_when_missing()
    {
        IProductRepository repo = new MemoryProductRepository();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.DeleteAsync(99));
    }

    [Fact]
    public async Task UpdateAsync_changes_matching_product()
    {
        IProductRepository repo = new MemoryProductRepository();
        await repo.AddAsync(new Product(1, "筆記本", 100m));
        await repo.AddAsync(new Product(2, "原子筆", 20m));

        await repo.UpdateAsync(new Product(1, "精裝筆記本", 150m));

        var list = await repo.AllAsync();
        Assert.Equal(2, list.Count);
        Assert.Equal("精裝筆記本", list[0].Name);
        Assert.Equal(150m, list[0].Price);
        Assert.Equal("原子筆", list[1].Name);
    }

    [Fact]
    public async Task UpdateAsync_throws_when_missing()
    {
        IProductRepository repo = new MemoryProductRepository();

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => repo.UpdateAsync(new Product(99, "幽靈", 1m)));
    }
}
