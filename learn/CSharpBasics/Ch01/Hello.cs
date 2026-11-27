namespace CSharpBasics.Ch01;

public static class Hello
{
    // 登入後的招呼語
    public static void WriteWelcome(string name)
    {
        Console.WriteLine("Hello, " + name + "!");
    }

    // 任務清單旁邊的進度
    public static void WriteProgress(int done, int total)
    {
        Console.Write(done);
        Console.Write("/");
        Console.Write(total);
    }

    // 登入後左上角名片
    public static void WriteCard(string name, string title)
    {
        Console.WriteLine("==========");
        Console.WriteLine(name);
        Console.WriteLine(title);
        Console.WriteLine("==========");
    }
}
