namespace CreationSharingPlatform.Configuration;

public static class SystemsManagerPaths
{
    public const string TeamCspCore = "/TeamCsp/Core/";
    public const string TeamCspPlatform = "/TeamCsp/Platform/";

    public static string[] AllPaths => new[] { TeamCspCore, TeamCspPlatform };
}
