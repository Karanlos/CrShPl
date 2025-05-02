namespace CreationSharingPlatform.Configuration;

public class IdentityServiceProviderConfiguration
{
    public const string SectionName = "IdentityServiceProvider";

    public string BaseUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string LoginCallbackPath { get; set; } = null!;
    public string LogoutCallbackPath { get; set; } = null!;
    public string? Authority => BaseUrl;
    public string Audience => $"{BaseUrl}/api";
}
