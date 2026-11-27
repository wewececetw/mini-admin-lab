using MiniAdmin.Core.Common;

namespace MiniAdmin.Core.Tests.Common;

public class PagingTests
{
    [Fact]
    public void PageCount_25筆_每頁10筆_要3頁()
    {
        Assert.Equal(3, Paging.PageCount(25, 10));
    }

    [Fact]
    public void PageCount_20筆_每頁10筆_剛好2頁()
    {
        Assert.Equal(2, Paging.PageCount(20, 10));
    }

    [Fact]
    public void PageCount_0筆_0頁()
    {
        Assert.Equal(0, Paging.PageCount(0, 10));
    }
}
