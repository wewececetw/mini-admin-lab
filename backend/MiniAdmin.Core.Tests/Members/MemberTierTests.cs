using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberTierTests
{
    [Fact]
    public void LevelName_用Enum_銅銀金()
    {
        Assert.Equal("銅", MemberLevel.LevelName(MemberTier.Bronze));
        Assert.Equal("銀", MemberLevel.LevelName(MemberTier.Silver));
        Assert.Equal("金", MemberLevel.LevelName(MemberTier.Gold));
    }
}
