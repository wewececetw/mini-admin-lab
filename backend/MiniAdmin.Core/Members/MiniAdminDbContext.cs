using Microsoft.EntityFrameworkCore;

namespace MiniAdmin.Core.Members;

public class MiniAdminDbContext : DbContext
{
    public MiniAdminDbContext(DbContextOptions<MiniAdminDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
}
