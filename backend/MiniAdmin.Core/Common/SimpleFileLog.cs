namespace MiniAdmin.Core.Common;

public static class SimpleFileLog
{
    public static void Append(string path, string message)
    {
        File.AppendAllText(path, message + Environment.NewLine);
    }
}

