namespace SeleniumAutomation.Tests.Configuration;

public sealed record TestSettings(Uri BaseUrl, bool Headless)
{
    public static TestSettings FromEnvironment()
    {
        string baseUrl = Environment.GetEnvironmentVariable("TEST_BASE_URL") ?? "https://www.saucedemo.com/";
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("TEST_BASE_URL must be an absolute HTTP or HTTPS URL.");
        }

        string headlessValue = Environment.GetEnvironmentVariable("TEST_HEADLESS") ?? "false";
        if (!bool.TryParse(headlessValue, out bool headless))
        {
            throw new ArgumentException("TEST_HEADLESS must be true or false.");
        }

        return new TestSettings(uri, headless);
    }
}
