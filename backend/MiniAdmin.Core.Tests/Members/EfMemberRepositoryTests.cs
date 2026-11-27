using Microsoft.EntityFrameworkCore;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class EfMemberRepositoryTests
{
    private static MiniAdminDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<MiniAdminDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;
        var db = new MiniAdminDbContext(options);
        db.Database.OpenConnection();
        db.Database.EnsureCreated();
        return db;
    }

    [Fact]
    public async Task AddAsync_之後資料庫找得到()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);

        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));

        var found = await db.Members.SingleAsync(m => m.Id == 1);
        Assert.Equal("王小明", found.Name);
        Assert.Equal("ming@example.com", found.Email);
    }

    [Fact]
    public async Task GetByIdAsync_能依Id找到()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);
        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));
        await repo.AddAsync(new Member(2, "李小華", "hua@example.com"));

        var found = await repo.GetByIdAsync(2);

        Assert.Equal(2, found.Id);
        Assert.Equal("李小華", found.Name);
        Assert.Equal("hua@example.com", found.Email);
    }

    [Fact]
    public async Task GetByIdAsync_找不到就丟KeyNotFoundException()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.GetByIdAsync(99));
    }

    [Fact]
    public async Task AllAsync_回傳目前所有會員()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);
        await repo.AddAsync(new Member(1, "王小明", "ming@example.com"));
        await repo.AddAsync(new Member(2, "李小華", "hua@example.com"));

        var all = await repo.AllAsync();

        Assert.Equal(2, all.Count);
        Assert.Equal(1, all[0].Id);
        Assert.Equal(2, all[1].Id);
    }

    [Fact]
    public async Task AllAsync_沒資料回傳空清單()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);

        var all = await repo.AllAsync();

        Assert.Empty(all);
    }

    [Fact]
    public async Task UpdateAsync_會改到已存在的會員()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);
        await repo.AddAsync(new Member(1, "Ada", "ada@example.com"));

        await repo.UpdateAsync(new Member(1, "Ada Lovelace", "ada.lovelace@example.com"));

        var found = await repo.GetByIdAsync(1);
        Assert.Equal("Ada Lovelace", found.Name);
        Assert.Equal("ada.lovelace@example.com", found.Email);
    }

    [Fact]
    public async Task UpdateAsync_找不到就丟KeyNotFoundException()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => repo.UpdateAsync(new Member(99, "Ghost", "ghost@example.com")));
    }

    [Fact]
    public async Task DeleteAsync_刪掉後GetById會找不到()
    {
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);
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
        await using var db = CreateDb();
        IMemberRepository repo = new EfMemberRepository(db);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.DeleteAsync(99));
    }
}
