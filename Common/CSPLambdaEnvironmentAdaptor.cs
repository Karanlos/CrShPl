namespace CreationSharingPlatform.Common;

public class CSPLambdaEnvironmentAdaptor : ICSPLambdaEnvironment
{
    public HostingContextName HostingContextName => CSPLambdaEnvironment.HostingContextName;

    public string EnvironmentName => CSPLambdaEnvironment.EnvironmentName;

    public EnvironmentType EnvironmentType => CSPLambdaEnvironment.EnvironmentType;

    public bool IsAws() => CSPLambdaEnvironment.IsAws();

    public bool IsDev() => CSPLambdaEnvironment.IsDev();

    public bool IsGithubAction() => CSPLambdaEnvironment.IsGithubAction();

    public bool IsLocal() => CSPLambdaEnvironment.IsLocal();

    public bool IsProduction() => CSPLambdaEnvironment.IsProduction();

    public bool IsQa() => CSPLambdaEnvironment.IsQa();

    public bool IsXUnit() => CSPLambdaEnvironment.IsXUnit();
}
