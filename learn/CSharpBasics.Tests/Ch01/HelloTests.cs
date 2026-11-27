using CSharpBasics.Ch01;

namespace CSharpBasics.Tests.Ch01;

public class HelloTests
{
    private static readonly string NL = Environment.NewLine;

    private static string CaptureConsole(Action action)
    {
        var original = Console.Out;
        var writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(original);
        }
        return writer.ToString();
    }

    [Fact]
    public void WriteWelcome_印出招呼語並換行()
    {
        var output = CaptureConsole(() => Hello.WriteWelcome("Barron"));

        Assert.Equal($"Hello, Barron!{NL}", output);
    }

    [Fact]
    public void WriteProgress_印出進度但不換行()
    {
        var output = CaptureConsole(() => Hello.WriteProgress(3, 10));

        Assert.Equal("3/10", output);
    }

    [Fact]
    public void WriteCard_印出四行名片()
    {
        var output = CaptureConsole(() => Hello.WriteCard("Barron", "Backend"));

        Assert.Equal($"=========={NL}Barron{NL}Backend{NL}=========={NL}", output);
    }
}
