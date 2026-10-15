using NUnit.Framework;
using SeleniumAutomation.Examples.Support;

namespace SeleniumAutomation.Examples.NUnit;

public class ArtifactRetentionTests
{
    [Test]
    public void CleanupKeepsFiveLatestRunsWithTheirFilesAndPreservesUnrelatedFolders()
    {
        string root = Path.Combine(Path.GetTempPath(), $"selenium-retention-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        try
        {
            for (int run = 1; run <= 7; run++)
            {
                foreach (string category in new[] { "reports", "screenshots", "logs" })
                {
                    string categoryDirectory = Path.Combine(root, $"run_{run:D2}", category);
                    Directory.CreateDirectory(categoryDirectory);
                    File.WriteAllText(Path.Combine(categoryDirectory, "example.txt"), "test artifact");
                }
            }

            Directory.CreateDirectory(Path.Combine(root, "samples"));
            ArtifactRetention.KeepLatestRuns(root);

            Assert.That(Directory.GetDirectories(root).Select(Path.GetFileName),
                Is.EquivalentTo(new[] { "run_03", "run_04", "run_05", "run_06", "run_07", "samples" }));
            Assert.That(Directory.GetFiles(root, "example.txt", SearchOption.AllDirectories), Has.Length.EqualTo(15));
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }
}
