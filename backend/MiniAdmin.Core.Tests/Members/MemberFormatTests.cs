using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberFormatTests
{
    private static readonly string NL = Environment.NewLine;

    [Fact]
    public void MemberCode_Id1_補滿4位數()
    {
        Assert.Equal("M0001", MemberFormat.MemberCode(1));
    }

    [Fact]
    public void MemberCode_Id1234_剛好4位數()
    {
        Assert.Equal("M1234", MemberFormat.MemberCode(1234));
    }

    [Fact]
    public void MaskEmail_小老鼠前面只留前2個字()
    {
        Assert.Equal("mi**@example.com", MemberFormat.MaskEmail("ming@example.com"));
    }

    [Fact]
    public void MaskEmail_名字比較長_星號跟著變多()
    {
        Assert.Equal("ba****@test.com", MemberFormat.MaskEmail("barron@test.com"));
    }

    [Fact]
    public void BuildCard_組出三行會員卡()
    {
        var card = MemberFormat.BuildCard(1, "王小明", "ming@example.com");

        Assert.Equal($"會員編號：M0001{NL}姓名：王小明{NL}Email：mi**@example.com{NL}", card);
    }
}
