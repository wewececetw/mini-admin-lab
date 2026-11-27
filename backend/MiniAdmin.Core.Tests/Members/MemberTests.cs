using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberTests
{
    [Fact]
    public void 建立會員後Id_Name_Email都有值()
    {
        var member = new Member(1, "王小明", "ming@example.com");

        Assert.Equal(1, member.Id);
        Assert.Equal("王小明", member.Name);
        Assert.Equal("ming@example.com", member.Email);
    }

    [Fact]
    public void Code_會員1號是M0001()
    {
        var member = new Member(1, "王小明", "ming@example.com");

        Assert.Equal("M0001", member.Code());
    }
}
