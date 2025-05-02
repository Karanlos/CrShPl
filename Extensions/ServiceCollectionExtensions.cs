using System.Text.Json;
using System.Text.Json.Serialization;
using CreationSharingPlatform.Common;
using CreationSharingPlatform.Configuration;
using CreationSharingPlatform.Managers;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Extensions.Hosting;

namespace CreationSharingPlatform.Extensions;

public static class ServiceCollectionExtensions
{
    public static IHostBuilder AddElasticLogging(this IHostBuilder hostBuilder,
        string project,
        string? applicationName = null)
    {

        return hostBuilder.ConfigureAppConfiguration((context, _) =>
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", CSPLambdaEnvironment.EnvironmentName)
                .Enrich.WithProperty("Project", project.Trim())
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("ConsoleFormatter", new EcsTextFormatter())
                .WriteTo.Console()
                .CreateBootstrapLogger();
        });
    }


    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCSPOptions(config);

        var coreConfig = config.Get<CoreConfiguration>();
        var identityProviderConfig = config.GetSection(IdentityServiceProviderConfiguration.SectionName).Get<IdentityServiceProviderConfiguration>();

        services.AddManagers();

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Adds a default converter so enums are converted to/from strings instead of numbers.
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
        // Configure OpenAPI
        services.AddSwaggerGen();

        services.AddHealthChecks();

        services.AddSingleton(new DiagnosticContext(Log.Logger));

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.CreationReadPermission, policy => {
                policy.RequireAuthenticatedUser();
            });
        });
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.Conventions.Add(new VersionByNamespaceConvention());
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = identityProviderConfig.BaseUrl;
                options.Audience = identityProviderConfig.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }

    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<ICreationsManager, CreationsManager>();
        services.AddScoped<IPartsManager, PartsManager>();
        services.AddScoped<IReportsManager, ReportsManager>();

        return services;
    }

    private static IServiceCollection AddCSPOptions(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<CoreConfiguration>(config)
            .Configure<IdentityServiceProviderConfiguration>(config.GetSection(IdentityServiceProviderConfiguration.SectionName));
    }

}
