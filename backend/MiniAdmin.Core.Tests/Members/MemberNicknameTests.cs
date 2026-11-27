using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberNicknameTests
{
    [Fact]
    public void 新建會員_暱稱是null()
    {
        var member = new Member(1, "王小明", "ming@example.com");

        Assert.Null(member.Nickname);
    }

    [Fact]
    public void 暱稱可以之後再填()
    {
        var member = new Member(1, "王小明", "ming@example.com");

        member.Nickname = "阿明";

        Assert.Equal("阿明", member.Nickname);
    }
}
