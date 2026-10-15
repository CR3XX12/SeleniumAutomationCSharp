using System.Globalization;
using System.Text.Json;

namespace SeleniumAutomation.Tests.TestData.Content;

internal static class WebsiteContentLoader
{
    public static WebsiteContent Load(string locale = "en-US")
    {
        // Resolve a culture name, rather than accepting arbitrary file paths.
        string cultureName = CultureInfo.GetCultureInfo(locale).Name;
        string path = Path.Combine(AppContext.BaseDirectory, "TestData", "Content", $"{cultureName}.json");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Website content for '{cultureName}' was not found: {path}", path);
        }

        WebsiteContent content;
        try
        {
            content = JsonSerializer.Deserialize<WebsiteContent>(File.ReadAllText(path),
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                ?? throw new JsonException("The content document must be an object, not null.");
        }
        catch (JsonException exception)
        {
            throw new InvalidOperationException($"Invalid website content in '{path}': {exception.Message}", exception);
        }

        if (content.Login is null || content.Products is null)
        {
            throw new InvalidOperationException($"Website content in '{path}' requires non-null 'login' and 'products' sections.");
        }

        RequireText(content.Login.InvalidCredentials, "login.invalidCredentials", path);
        RequireText(content.Login.LockedUser, "login.lockedUser", path);
        RequireText(content.Products.Heading, "products.heading", path);
        return content;
    }

    private static void RequireText(string? value, string key, string path)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Website content '{key}' must contain text in '{path}'.");
        }
    }
}
