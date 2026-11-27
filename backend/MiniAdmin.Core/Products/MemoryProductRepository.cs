namespace MiniAdmin.Core.Products;

public class MemoryProductRepository : IProductRepository
{
    private List<Product> _items = new List<Product>();
    public Task AddAsync(Product product)
    {
        _items.Add(product);
        return Task.CompletedTask;

    }

    public Task<List<Product>> AllAsync()
    {
        return Task.FromResult(_items);
    }

    public Task UpdateAsync(Product product)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == product.Id)
            {
                _items[i].Name = product.Name;
                _items[i].Price = product.Price;
                return Task.CompletedTask;
            }
        }
        throw new KeyNotFoundException("找不到商品");
    }

    public Task DeleteAsync(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == id)
            {
                _items.RemoveAt(i);
                return Task.CompletedTask;
            }
        }
        throw new KeyNotFoundException("找不到商品");
    }
}
