namespace MiniAdmin.Core.Products;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public decimal PriceWithTax()
    {
        return Pricing.WithTax(Price);
    }
}
