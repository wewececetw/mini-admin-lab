using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberRepositoryListTests
{
    [Fact]
    public async Task All_回傳List而且Count正確()
    {
        IMemberRepository repo = new MemoryMemberRepository();
        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));
        await repo.AddAsync(new Member(2, "李小華", "hua@example.com"));

        List<Member> all = await repo.AllAsync();

        Assert.Equal(2, all.Count);
        Assert.Equal(1, all[0].Id);
        Assert.Equal(2, all[1].Id);
    }
}
