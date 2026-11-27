using MiniAdmin.Core.Common;

namespace MiniAdmin.Core.Members;

public static class MemberQuery
{
    public static List<Member> ByNameContains(List<Member> all, string keyword)
    {
        List<Member> members = all.Where(n => n.Name.Contains(keyword)).ToList();
        return members;
    }

    public static List<Member> SortByName(List<Member> all)
    {
        List<Member> members = all.OrderBy(s => s.Name).ToList();

        return members;
    }

    public static PagedResult<Member> Page(List<Member> all, int page, int pageSize)
    {
        PagedResult<Member> memberPage = new PagedResult<Member>();

        int start = (page - 1) * pageSize;

        memberPage.Items = all.Skip(start).Take(pageSize).ToList();

        memberPage.TotalCount = all.Count;

        memberPage.Page = page;

        memberPage.PageSize = pageSize;

        return memberPage;
    }

    public static PagedResult<Member> Search(List<Member> all, string keyword, int page, int pageSize)
    {
        PagedResult<Member> memberPage = new PagedResult<Member>();

        List<Member> members = MemberQuery.ByNameContains(all, keyword);
        members = MemberQuery.SortByName(members);

        return MemberQuery.Page(members, page, pageSize);
    }
}
