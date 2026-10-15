using NUnit.Framework;
using SeleniumAutomation.Examples.Support;

namespace SeleniumAutomation.Examples;

[SetUpFixture]
public class ArtifactRunSetup
{
    [OneTimeSetUp]
    public void StartRun()
    {
        TestContext.Progress.WriteLine($"Run artifacts: {ExamplePaths.RunDirectory}");
    }

    [OneTimeTearDown]
    public void FinishRun()
    {
        ExamplePaths.CleanOldRuns();
    }
}
