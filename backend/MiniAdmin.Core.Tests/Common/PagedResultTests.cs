using MiniAdmin.Core.Common;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Common;

public class PagedResultTests
{
    [Fact]
    public void Create_第1頁_每頁2筆_只拿前兩筆()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "王一", "a@example.com"));
        all.Add(new Member(2, "王二", "b@example.com"));
        all.Add(new Member(3, "王三", "c@example.com"));

        PagedResult<Member> page = Paging.CreatePage(all, page: 1, pageSize: 2);

        Assert.Equal(3, page.TotalCount);
        Assert.Equal(2, page.Items.Count);
        Assert.Equal(1, page.Items[0].Id);
        Assert.Equal(2, page.Items[1].Id);
        Assert.Equal(2, page.TotalPages);
    }

    [Fact]
    public void Create_第2頁_拿剩下的()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "王一", "a@example.com"));
        all.Add(new Member(2, "王二", "b@example.com"));
        all.Add(new Member(3, "王三", "c@example.com"));

        PagedResult<Member> page = Paging.CreatePage(all, page: 2, pageSize: 2);

        Assert.Equal(1, page.Items.Count);
        Assert.Equal(3, page.Items[0].Id);
    }

    [Fact]
    public void Filter_用lambda只留名字有王的()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "王小明", "a@example.com"));
        all.Add(new Member(2, "李小華", "b@example.com"));
        all.Add(new Member(3, "王大同", "c@example.com"));

        List<Member> kept = Paging.Filter(all, m => m.Name.Contains("王"));

        Assert.Equal(2, kept.Count);
        Assert.Equal(1, kept[0].Id);
        Assert.Equal(3, kept[1].Id);
    }
}
