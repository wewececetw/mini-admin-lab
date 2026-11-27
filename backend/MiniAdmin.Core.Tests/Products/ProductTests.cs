using MiniAdmin.Core.Products;

namespace MiniAdmin.Core.Tests.Products;

public class ProductTests
{
    [Fact]
    public void 建立商品後Name和Price都有值()
    {
        var product = new Product(1, "筆記本", 100m);

        Assert.Equal(1, product.Id);
        Assert.Equal("筆記本", product.Name);
        Assert.Equal(100m, product.Price);
    }

    [Fact]
    public void PriceWithTax_100元含稅是105元()
    {
        var product = new Product(1, "筆記本", 100m);

        Assert.Equal(105m, product.PriceWithTax());
    }
}
