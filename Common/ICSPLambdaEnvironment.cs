namespace CreationSharingPlatform.Common;

public interface ICSPLambdaEnvironment
{
    /// <summary>
    /// The hosting context name of this app set by the "HostingContext" environment variable. Default is Local.
    /// </summary>
    HostingContextName HostingContextName { get; }

    /// <summary>
    /// Environment name set by ASPNETCORE_ENVIRONMENT or related base setup. Defaults to "Unknown-Dev".
    /// Format: <c>(name)-(type)</c>.
    /// </summary>
    /// <remarks>
    /// There are no constraints on <c>name</c> except that "Stable" has a special meaning.<br/>
    /// <c>type</c> is the <see cref="EnvironmentType"/>.
    /// </remarks>
    string EnvironmentName { get; }

    /// <summary>
    /// The last part (after the dash) of the environment name gives the type.
    /// </summary>
    EnvironmentType EnvironmentType { get; }

    /// <summary>
    /// Is this app running in a Local hosting context?
    /// </summary>
    bool IsLocal();

    /// <summary>
    /// Is this app running in a GitHub Action hosting context?
    /// </summary>
    bool IsGithubAction();

    /// <summary>
    /// Is this app running in an AWS hosting context?
    /// </summary>
    bool IsAws();

    /// <summary>
    /// Is this a Dev environment type?
    /// </summary>
    bool IsDev();

    /// <summary>
    /// Is this a Qa environment type?
    /// </summary>
    bool IsQa();

    /// <summary>
    /// Is this a Live environment type?
    /// </summary>
    bool IsProduction();

    /// <summary>
    /// Is this code running as part of an xUnit test?
    /// </summary>
    bool IsXUnit();
}
