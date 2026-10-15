using NUnit.Framework;
using SeleniumAutomation.Tests.Infrastructure;

namespace SeleniumAutomation.Tests;

[SetUpFixture]
public class TestRun
{
    internal static RunReport Report { get; private set; } = null!;

    public static string DirectoryPath { get; } = Path.Combine(
        AppContext.BaseDirectory, "artifacts", "runs",
        $"run_{DateTime.UtcNow:yyyyMMdd_HHmmss_fffffff}_{Guid.NewGuid():N}");

    public static string Artifact(string category, string fileName)
    {
        string directory = Path.Combine(DirectoryPath, category);
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, fileName);
    }

    [OneTimeSetUp]
    public void StartRun()
    {
        Directory.CreateDirectory(DirectoryPath);
        TestContext.Progress.WriteLine($"Run artifacts: {DirectoryPath}");
        CleanOldRuns();
        Report = new RunReport();
    }

    [OneTimeTearDown]
    public void FinishRun()
    {
        try
        {
            Report.Flush();
        }
        finally
        {
            CleanOldRuns();
        }
    }

    private static void CleanOldRuns()
    {
        var oldRuns = new DirectoryInfo(Path.GetDirectoryName(DirectoryPath)!)
            .EnumerateDirectories("run_*")
            .Where(directory => !directory.Attributes.HasFlag(FileAttributes.ReparsePoint))
            .OrderByDescending(directory => directory.Name, StringComparer.Ordinal)
            .Skip(5)
            .ToArray();

        foreach (var directory in oldRuns)
        {
            directory.Delete(recursive: true);
        }
    }
}
