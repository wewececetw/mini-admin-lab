using MiniAdmin.Core.Products;

namespace MiniAdmin.Core.Tests.Products;

public class ProductIdsTests
{
    private static readonly int[] Ids = [101, 102, 103];

    [Fact]
    public void Get_index0是第一個商品Id()
    {
        Assert.Equal(101, ProductIds.Get(Ids, 0));
    }

    [Fact]
    public void Get_index2是第三個商品Id()
    {
        Assert.Equal(103, ProductIds.Get(Ids, 2));
    }

    [Fact]
    public void Last_取最後一個商品Id()
    {
        Assert.Equal(103, ProductIds.Last(Ids));
    }
}
