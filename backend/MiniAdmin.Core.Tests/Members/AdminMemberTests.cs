using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class AdminMemberTests
{
    [Fact]
    public void 一般會員_CanManageMembers是false()
    {
        var member = new Member(1, "王小明", "ming@example.com");

        Assert.False(member.CanManageMembers());
    }

    [Fact]
    public void 管理員_CanManageMembers是true()
    {
        var admin = new AdminMember(2, "管理員", "admin@example.com");

        Assert.True(admin.CanManageMembers());
        Assert.Equal(2, admin.Id);
        Assert.Equal("管理員", admin.Name);
    }
}
