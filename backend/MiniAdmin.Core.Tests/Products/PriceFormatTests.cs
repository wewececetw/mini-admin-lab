using MiniAdmin.Core.Products;

namespace MiniAdmin.Core.Tests.Products;

public class PriceFormatTests
{
    [Fact]
    public void FormatPrice_加上幣別與千分位()
    {
        Assert.Equal("NT$ 1,234", Pricing.FormatPrice(1234m));
    }
}
