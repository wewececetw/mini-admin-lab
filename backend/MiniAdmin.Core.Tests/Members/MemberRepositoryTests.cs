using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberRepositoryTests
{
    [Fact]
    public async Task Add之後_GetByIdAsync能找到()
    {
        IMemberRepository repo = new MemoryMemberRepository();
        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));

        var found = await repo.GetByIdAsync(1);

        Assert.Equal("王小明", found.Name);
    }

    [Fact]
    public async Task All_回傳目前所有會員()
    {
        IMemberRepository repo = new MemoryMemberRepository();
        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));
        await repo.AddAsync(new AdminMember(2, "管理員", "admin@example.com"));

        var all = await repo.AllAsync();

        Assert.Equal(2, all.Count);
        Assert.Equal(1, all[0].Id);
        Assert.Equal(2, all[1].Id);
    }

    [Fact]
    public async Task UpdateAsync_會改到已存在的會員()
    {
        IMemberRepository repo = new MemoryMemberRepository();
        await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));

        await repo.UpdateAsync(new Member(1, "Ada Lovelace", "ada.lovelace@example.com"));

        var found = await repo.GetByIdAsync(1);
        Assert.Equal("Ada Lovelace", found.Name);
        Assert.Equal("ada.lovelace@example.com", found.Email);
    }

    [Fact]
    public async Task UpdateAsync_找不到就丟KeyNotFoundException()
    {
        IMemberRepository repo = new MemoryMemberRepository();

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => repo.UpdateAsync(new Member(99, "Ghost", "ghost@example.com")));
    }

    [Fact]
    public async Task DeleteAsync_刪掉後GetById會找不到()
    {
        IMemberRepository repo = new MemoryMemberRepository();
        await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));
        await repo.AddAsync(new Member(2, "Bob", "bob@example.com"));

        await repo.DeleteAsync(1);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.GetByIdAsync(1));
        var bob = await repo.GetByIdAsync(2);
        Assert.Equal("Bob", bob.Name);
    }

    [Fact]
    public async Task DeleteAsync_找不到就丟KeyNotFoundException()
    {
        IMemberRepository repo = new MemoryMemberRepository();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.DeleteAsync(99));
    }
}
