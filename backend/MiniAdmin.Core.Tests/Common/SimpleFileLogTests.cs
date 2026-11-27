using MiniAdmin.Core.Common;

namespace MiniAdmin.Core.Tests.Common;

public class SimpleFileLogTests
{
    [Fact]
    public void Append_會在檔案末尾加一行()
    {
        var path = Path.Combine(Path.GetTempPath(), $"mini-admin-log-{Guid.NewGuid():N}.txt");

        try
        {
            SimpleFileLog.Append(path, "第一行");
            SimpleFileLog.Append(path, "第二行");

            var text = File.ReadAllText(path);
            Assert.Contains("第一行", text);
            Assert.Contains("第二行", text);
        }
        finally
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
