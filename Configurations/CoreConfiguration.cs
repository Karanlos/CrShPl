namespace CreationSharingPlatform.Configuration;

public class CoreConfiguration
{
    public const string SectionName = "CoreConfiguration";

    public string? LoggingApiKey { get; set; }
    public string DomainName { get; set; } = null!;
    public string LoginCallbackPath { get; set; } = null!;
    public string LogoutCallbackPath { get; set; } = null!;
    public string? DuendeLicense { get; set; }
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;

    public static class OidcConfiguration
    {
        public static readonly string[] Scopes = new[]
        {
            "openid",
            "email",
            "profile",
            "edit-user",
            "offline_access",
            "user-security-audit",
            "urn:team-csp:custom-account-nickname:user.update",
        };
        public const string ResponseType = "client";
        public const string ResponseMode = "query";
    }
}
