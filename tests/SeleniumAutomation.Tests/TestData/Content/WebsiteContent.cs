namespace SeleniumAutomation.Tests.TestData.Content;

public sealed record WebsiteContent
{
    public required LoginContent Login { get; init; }
    public required ProductsContent Products { get; init; }
}

public sealed record LoginContent
{
    public required string InvalidCredentials { get; init; }
    public required string LockedUser { get; init; }
}

public sealed record ProductsContent
{
    public required string Heading { get; init; }
}
