namespace MiniAdmin.Core.Products;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<List<Product>> AllAsync();
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
