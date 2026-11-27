using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberLevelTests
{
    [Fact]
    public void LevelName_等級1是銅()
    {
        Assert.Equal("銅", MemberLevel.LevelName(1));
    }

    [Fact]
    public void LevelName_等級2是銀()
    {
        Assert.Equal("銀", MemberLevel.LevelName(2));
    }

    [Fact]
    public void LevelName_等級3是金()
    {
        Assert.Equal("金", MemberLevel.LevelName(3));
    }

    [Fact]
    public void LevelName_其他等級是未知()
    {
        Assert.Equal("未知", MemberLevel.LevelName(0));
        Assert.Equal("未知", MemberLevel.LevelName(4));
    }
}
