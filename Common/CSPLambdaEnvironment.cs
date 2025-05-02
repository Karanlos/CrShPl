namespace CreationSharingPlatform.Common;

public enum HostingContextName
{
    Local,
    GithubAction,
    AWS
}

/// <summary>
/// The last part (after the dash) of the environment name gives the type.
/// </summary>
public enum EnvironmentType
{
    Dev,
    Qa,
    Live
}

/// <summary>
/// All things related to environment detection.
/// </summary>
public static class CSPLambdaEnvironment
{
    private static string _environmentNameUpper;

    /// <summary>
    /// <see cref="EnvironmentName"/> value used if none found.
    /// </summary>
    public const string DEFAULT_ENVIRONMENT_NAME = "local";

    /// <summary>
    /// <see cref="AwsRegion"/> value used if none found.
    /// </summary>
    public const string DEFAULT_AWS_REGION = "eu-west-1";

    /// <summary>
    /// The hosting context name of this app set by the "HostingContext" environment variable. Default is Local.
    /// </summary>
    public static HostingContextName HostingContextName { get; }

    /// <summary>
    /// Environment name set by ASPNETCORE_ENVIRONMENT or related base setup. Defaults to "Unknown-Dev".
    /// Format: <c>(name)-(type)</c>.
    /// </summary>
    /// <remarks>
    /// There are no constraints on <c>name</c> except that "Stable" has a special meaning.<br/>
    /// <c>type</c> is the <see cref="EnvironmentType"/>.
    /// </remarks>
    public static string EnvironmentName { get; }

    /// <summary>
    /// The last part (after the dash) of the environment name gives the type.
    /// </summary>
    public static EnvironmentType EnvironmentType { get; }

    /// <summary>
    /// AWS profile taken from environment variable "AWS_PROFILE".
    /// </summary>
    /// <remarks>
    /// AWS profiles are only used in non-AWS hosting contexts.
    /// </remarks>
    public static string? AwsProfile { get; }

    /// <summary>
    /// AWS region taken from environment variable "AWS_REGION".
    /// Defaults to DEFAULT_AWS_REGION.
    /// </summary>
    public static string AwsRegion { get; }


    // Static constructor
    static CSPLambdaEnvironment()
    {
        int tid = Environment.CurrentManagedThreadId;
        Environment.SetEnvironmentVariable("AWS:Profile", Environment.GetEnvironmentVariable("AWS_PROFILE"));

        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .AddEnvironmentVariables(prefix: "DOTNETCORE_")
            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
            .AddCommandLine(Environment.GetCommandLineArgs())
            .Build();

        EnvironmentName = config["ENVIRONMENT"] ?? DEFAULT_ENVIRONMENT_NAME;
        _environmentNameUpper = EnvironmentName.ToUpperInvariant();

        EnvironmentType = GetEnvironmentType(_environmentNameUpper);

        HostingContextName = GetHostingContextName(config["HostingContext"]);

        AwsProfile = config["AWS_PROFILE"];
        AwsRegion = config["AWS_REGION"] ?? DEFAULT_AWS_REGION;

        Console.WriteLine($"==({tid})= {nameof(CSPLambdaEnvironment)}.{nameof(EnvironmentName)}='{EnvironmentName}'");
        Console.WriteLine($"==({tid})= {nameof(CSPLambdaEnvironment)}.{nameof(HostingContextName)}='{HostingContextName}'");
        Console.WriteLine($"==({tid})= {nameof(CSPLambdaEnvironment)}.{nameof(AwsProfile)}='{AwsProfile}'");
        Console.WriteLine($"==({tid})= {nameof(CSPLambdaEnvironment)}.{nameof(AwsRegion)}='{AwsRegion}'");
    }


    /// <summary>
    /// To sets AWS:Profile environment variable before
    /// any other calls initialize the static constructor, call this method.
    /// </summary>
    public static void Initialize()
    {
        // Do nothing, will invoke the static constructor
    }

    /// <summary>
    /// Get a non-static instance of CSP environment setup
    /// to use for dependecy injection.
    /// </summary>
    public static ICSPLambdaEnvironment CreateAdaptor()
    {
        return new CSPLambdaEnvironmentAdaptor();
    }

    #region --- HostingContext ------------------------------------------------------------

    /// <summary>
    /// Is this app running in a Local hosting context?
    /// </summary>
    public static bool IsLocal() => true; //HostingContextName == HostingContextName.Local;

    /// <summary>
    /// Is this app running in a GitHub Action hosting context?
    /// </summary>
    public static bool IsGithubAction() => HostingContextName == HostingContextName.GithubAction;

    /// <summary>
    /// Is this app running in an AWS hosting context?
    /// </summary>
    public static bool IsAws() => false; // HostingContextName == HostingContextName.AWS;


    private static HostingContextName GetHostingContextName(string? hostingContext)
    {
        string? val = hostingContext?.ToUpperInvariant();

        if (val == "GITHUBACTION")
            return HostingContextName.GithubAction;

        if (val == "AWS")
            return HostingContextName.AWS;

        return HostingContextName.Local;
    }

    #endregion --- HostingContext ------------------------------------------------------------

    #region --- Environment Name ------------------------------------------------------------

    /// <summary>
    /// Is this a Dev environment type?
    /// </summary>
    public static bool IsDev() => EnvironmentType == EnvironmentType.Dev;

    /// <summary>
    /// Is this a Qa environment type?
    /// </summary>
    public static bool IsQa() => EnvironmentType == EnvironmentType.Qa;

    /// <summary>
    /// Is this a Live environment type?
    /// </summary>
    public static bool IsProduction() => EnvironmentType == EnvironmentType.Live;

    private static EnvironmentType GetEnvironmentType(string environmentNameUpper)
    {
        string[] elements = environmentNameUpper.Split('-', 2);

        return elements.Last() switch
        {
            "QA" => EnvironmentType.Qa,
            "LIVE" => EnvironmentType.Live,
            _ => EnvironmentType.Dev
        };
    }

    #endregion --- Environment Name ------------------------------------------------------------

    /// <summary>
    /// Is this code running as part of an xUnit test?
    /// </summary>
    public static bool IsXUnit() =>
        AppDomain.CurrentDomain.GetAssemblies().Any(
            a => a.FullName.ToUpper().StartsWith("XUNIT.RUNNER"));
}
