namespace SeleniumAutomation.Examples.Support;

internal static class ArtifactRetention
{
    public static void KeepLatestRuns(string runsDirectory)
    {
        // Only generated run directories participate; never follow directory links.
        var oldRuns = new DirectoryInfo(runsDirectory)
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
