using MiniAdmin.Core.Common;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberQueryTests
{
    [Fact]
    public void ByNameContains_名字有關鍵字的留下()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "王小明", "a@example.com"));
        all.Add(new Member(2, "李小華", "b@example.com"));
        all.Add(new Member(3, "王大同", "c@example.com"));

        List<Member> kept = MemberQuery.ByNameContains(all, "王");

        Assert.Equal(2, kept.Count);
        Assert.Equal(1, kept[0].Id);
        Assert.Equal(3, kept[1].Id);
    }

    [Fact]
    public void ByNameContains_沒人符合_回空清單()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "李小華", "a@example.com"));

        List<Member> kept = MemberQuery.ByNameContains(all, "王");

        Assert.Empty(kept);
    }

    [Fact]
    public void SortByName_依名字由小到大()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "Charlie", "a@example.com"));
        all.Add(new Member(2, "Alice", "b@example.com"));
        all.Add(new Member(3, "Bob", "c@example.com"));

        List<Member> sorted = MemberQuery.SortByName(all);

        Assert.Equal(3, sorted.Count);
        Assert.Equal("Alice", sorted[0].Name);
        Assert.Equal("Bob", sorted[1].Name);
        Assert.Equal("Charlie", sorted[2].Name);
    }

    [Fact]
    public void Page_第1頁_每頁2筆()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "A", "a@example.com"));
        all.Add(new Member(2, "B", "b@example.com"));
        all.Add(new Member(3, "C", "c@example.com"));

        PagedResult<Member> result = MemberQuery.Page(all, page: 1, pageSize: 2);

        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(1, result.Items[0].Id);
        Assert.Equal(2, result.Items[1].Id);
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
    }

    [Fact]
    public void Page_第2頁_拿剩下的()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "A", "a@example.com"));
        all.Add(new Member(2, "B", "b@example.com"));
        all.Add(new Member(3, "C", "c@example.com"));

        PagedResult<Member> result = MemberQuery.Page(all, page: 2, pageSize: 2);

        Assert.Single(result.Items);
        Assert.Equal(3, result.Items[0].Id);
    }

    [Fact]
    public void Search_先篩再排再分頁_TotalCount是篩後筆數()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "Charlie", "a@example.com"));
        all.Add(new Member(2, "Alice", "b@example.com"));
        all.Add(new Member(3, "Bob", "c@example.com"));
        all.Add(new Member(4, "Ann", "d@example.com"));

        PagedResult<Member> result = MemberQuery.Search(all, keyword: "A", page: 1, pageSize: 1);

        Assert.Equal(2, result.TotalCount);
        Assert.Single(result.Items);
        Assert.Equal("Alice", result.Items[0].Name);
        Assert.Equal(1, result.Page);
        Assert.Equal(1, result.PageSize);
    }

    [Fact]
    public void Search_第2頁()
    {
        List<Member> all = new List<Member>();
        all.Add(new Member(1, "Charlie", "a@example.com"));
        all.Add(new Member(2, "Alice", "b@example.com"));
        all.Add(new Member(3, "Bob", "c@example.com"));
        all.Add(new Member(4, "Ann", "d@example.com"));

        PagedResult<Member> result = MemberQuery.Search(all, keyword: "A", page: 2, pageSize: 1);

        Assert.Equal(2, result.TotalCount);
        Assert.Single(result.Items);
        Assert.Equal("Ann", result.Items[0].Name);
    }
}
