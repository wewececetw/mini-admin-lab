using Microsoft.EntityFrameworkCore;

namespace MiniAdmin.Core.Members;

public class EfMemberRepository : IMemberRepository
{
    private readonly MiniAdminDbContext _db;

    public EfMemberRepository(MiniAdminDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Member member)
    {
        _db.Add(member);
        await _db.SaveChangesAsync();
    }

    public async Task<Member> GetByIdAsync(int id)
    {
        var member = await _db.Members.FindAsync(id);
        if (member == null)
        {
            throw new KeyNotFoundException("找不到會員");
        }
        return member;
    }

    public async Task<List<Member>> AllAsync()
    {
        return await _db.Members.ToListAsync();
    }

    public async Task UpdateAsync(Member member)
    {
        var list = await _db.Members.FindAsync(member.Id);

        if (list == null)
        {
            throw new KeyNotFoundException("找不到會員");
        }
        list.Name = member.Name;
        list.Email = member.Email;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var list = await _db.Members.FindAsync(id);
        if (list == null)
        {
            throw new KeyNotFoundException("找不到會員");
        }
        _db.Members.Remove(list);
        await _db.SaveChangesAsync();
    }
}
