namespace CreationSharingPlatform.Common;

public static class CSPConstants
{
    public const string CSP_AUTH_COOKIE_NAME = "CSP-Auth";
    public const string CSP_AUTH_COOKIE_PATH = "/";
    public const string CSP_AUTH_COOKIE_SAMESITE = "Strict";
    public const string CSP_AUTH_COOKIE_DOMAIN = ".teamcsp.com"; // Fake domain
}

public static class Policies
{
    public const string CreationReadPermission = "read:creation";
    public const string AdminPermission = "admin";
}
