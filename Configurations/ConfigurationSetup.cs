using Amazon;
using Amazon.Extensions.Configuration.SystemsManager;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Serilog;
using System.Reflection;
using CreationSharingPlatform.Common;

namespace CreationSharingPlatform.Configuration;

public static class ConfigurationSetup
{
    public static void AddCreationSharingPlatformConfiguration(this IConfigurationBuilder builder, Assembly assembly, params string[] paths)
    {
        builder.Sources.Clear();

        foreach (var path in paths)
        {
            builder.AddAwsSystemsManager(path, CSPLambdaEnvironment.IsAws(), CSPLambdaEnvironment.AwsRegion);
        }
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        builder.AddJsonFile($"appsettings.{CSPLambdaEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        // For local development. Allows the user to store secrets without having them in the repository code and configuration.
        if (CSPLambdaEnvironment.IsLocal())
        {
            builder.AddUserSecrets("teamcsp_npu_secrets", reloadOnChange: true);
            builder.AddUserSecrets(CSPLambdaEnvironment.EnvironmentName, reloadOnChange: true);
        }
    }

    private static void AddAwsSystemsManager(
        this IConfigurationBuilder configBuilder,
        string systemsManagerPath,
        bool isAwsHostingContext,
        string fallbackAwsRegion)
    {
        configBuilder
            .AddSystemsManager(configureSource =>
            {
                configureSource.Path = systemsManagerPath;
                configureSource.AwsOptions = new AWSOptions
                {
                    Credentials = FallbackCredentialsFactory.GetCredentials(true),
                    Region = FallbackRegionFactory.GetRegionEndpoint(true) ?? RegionEndpoint.GetBySystemName(fallbackAwsRegion),
                    DefaultClientConfig = { ResignRetries = true }
                };
                configureSource.Optional = !isAwsHostingContext;
                configureSource.ReloadAfter = TimeSpan.FromMinutes(15);
                configureSource.OnLoadException += exceptionContext =>
                {
                    Log.ForContext<SystemsManagerConfigurationProvider>().Fatal(exceptionContext.Exception,
                        "Failed loading config {systemsManagerPath}  from parameter store: {message}",
                        systemsManagerPath, exceptionContext.Exception.Message);
                };
            });
    }
}
