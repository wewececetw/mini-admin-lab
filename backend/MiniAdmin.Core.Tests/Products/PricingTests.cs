using MiniAdmin.Core.Products;

namespace MiniAdmin.Core.Tests.Products;

public class PricingTests
{
    [Fact]
    public void 營業稅率是百分之5()
    {
        Assert.Equal(0.05m, Pricing.TaxRate);
    }

    [Fact]
    public void WithTax_100元含稅是105元()
    {
        Assert.Equal(105m, Pricing.WithTax(100m));
    }

    [Fact]
    public void WithTax_指定稅率10趴_100元是110元()
    {
        Assert.Equal(110m, Pricing.WithTax(100m, 0.10m));
    }

    [Fact]
    public void ParseQuantity_表單送來的文字3轉成數字3()
    {
        Assert.Equal(3, Pricing.ParseQuantity("3"));
    }

    [Fact]
    public void WholeDollars_報表只顯示整數元_小數直接捨去()
    {
        Assert.Equal(99, Pricing.WholeDollars(99.9m));
    }

    [Fact]
    public void AverageRating_總共7顆星_2人評分_平均3點5顆()
    {
        Assert.Equal(3.5, Pricing.AverageRating(7, 2));
    }

    [Fact]
    public void ShippingFee_滿1000元免運()
    {
        Assert.Equal(0m, Pricing.ShippingFee(1000m));
    }

    [Fact]
    public void ShippingFee_未滿1000元運費60元()
    {
        Assert.Equal(60m, Pricing.ShippingFee(999m));
    }

    [Fact]
    public void ShippingFee_門檻可以指定_滿500元就免運()
    {
        Assert.Equal(0m, Pricing.ShippingFee(500m, 500m));
    }
}
