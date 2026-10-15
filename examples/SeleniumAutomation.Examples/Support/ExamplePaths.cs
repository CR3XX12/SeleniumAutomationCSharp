namespace SeleniumAutomation.Examples.Support;

internal static class ExamplePaths
{
    private static readonly Lazy<string> CurrentRun = new(CreateRunDirectory);

    public static string RunDirectory => CurrentRun.Value;

    public static string Resource(params string[] segments)
    {
        return Path.Combine(new[] { AppContext.BaseDirectory, "Resources" }.Concat(segments).ToArray());
    }

    public static string Artifact(string category, string fileName)
    {
        string directory = Path.Combine(RunDirectory, category);
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, fileName);
    }

    public static void CleanOldRuns()
    {
        if (CurrentRun.IsValueCreated)
        {
            ArtifactRetention.KeepLatestRuns(Path.GetDirectoryName(RunDirectory)!);
        }
    }

    private static string CreateRunDirectory()
    {
        string artifactRoot = Path.Combine(AppContext.BaseDirectory, "artifacts");
        string runsRoot = Path.Combine(artifactRoot, "runs");
        Directory.CreateDirectory(runsRoot);

        // Preserve output from the previous layout as one archive that also rotates out.
        string legacyDirectory = Path.Combine(runsRoot, "run_0000_legacy");
        foreach (string category in new[] { "reports", "screenshots", "logs" })
        {
            string oldDirectory = Path.Combine(artifactRoot, category);
            if (Directory.Exists(oldDirectory))
            {
                Directory.CreateDirectory(legacyDirectory);
                Directory.Move(oldDirectory, Path.Combine(legacyDirectory, category));
            }
        }

        string directory = Path.Combine(runsRoot, $"run_{DateTime.UtcNow:yyyyMMdd_HHmmss_fffffff}_{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        ArtifactRetention.KeepLatestRuns(runsRoot);
        return directory;
    }
}
