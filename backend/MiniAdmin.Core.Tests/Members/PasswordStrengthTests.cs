using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class PasswordStrengthTests
{
    [Fact]
    public void Check_少於8個字是弱()
    {
        Assert.Equal("弱", PasswordStrength.Check("abc1"));
    }

    [Fact]
    public void Check_剛好7個字有數字還是弱()
    {
        Assert.Equal("弱", PasswordStrength.Check("abcdef1"));
    }

    [Fact]
    public void Check_8個字以上沒有數字是中()
    {
        Assert.Equal("中", PasswordStrength.Check("abcdefgh"));
    }

    [Fact]
    public void Check_8個字以上有數字是強()
    {
        Assert.Equal("強", PasswordStrength.Check("abcdefg1"));
    }

    [Fact]
    public void Check_數字不在最後一個字也是強()
    {
        Assert.Equal("強", PasswordStrength.Check("abcdefg1x"));
    }
}
