namespace MiniAdmin.Api.Tests.Docker;

public class DockerfileTests
{
    [Fact]
    public void Backend_Dockerfile_builds_and_runs_Api()
    {
        var path = Path.Combine(FindRepoRoot(), "backend", "Dockerfile");
        Assert.True(File.Exists(path), $"找不到 {path}");

        var text = File.ReadAllText(path);
        Assert.Contains("mcr.microsoft.com/dotnet/sdk", text);
        Assert.Contains("mcr.microsoft.com/dotnet/aspnet", text);
        Assert.Contains("MiniAdmin.Api", text);
        Assert.Contains("MiniAdmin.Core", text);
        Assert.Contains("EXPOSE", text);
        Assert.Contains("MiniAdmin.Api.dll", text);
        Assert.True(
            text.Contains("ENTRYPOINT", StringComparison.Ordinal)
            || text.Contains("CMD", StringComparison.Ordinal),
            "要有 ENTRYPOINT 或 CMD");

    }

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "MiniAdmin.slnx")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        throw new InvalidOperationException("找不到 repo 根目錄（MiniAdmin.slnx）");
    }
}
