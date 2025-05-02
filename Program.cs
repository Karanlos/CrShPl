using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using CreationSharingPlatform.Configuration;
using CreationSharingPlatform.Lambda;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using CreationSharingPlatform.Extensions;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<HttpApiJsonSerilalizerContext>))]
[assembly: ApiController]

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureAppConfiguration(configBuilder =>
    {
        configBuilder.AddCreationSharingPlatformConfiguration(
            typeof(Program).Assembly,
            SystemsManagerPaths.TeamCspCore,
            SystemsManagerPaths.TeamCspPlatform);
    });

    builder.Host.AddElasticLogging("TeamCSP", "CreationSharingPlatform");

    builder.Host.UseDefaultServiceProvider(o =>
    {
        o.ValidateOnBuild = true;
        o.ValidateScopes = true;
    });

    builder.Services.AddServices(builder.Configuration);

    var app = builder.Build();

    app.UseHsts();

    app.UseSerilogRequestLogging();

    var forwardedHeadersOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        ForwardLimit = 2
    };
    forwardedHeadersOptions.KnownNetworks.Clear();
    forwardedHeadersOptions.KnownProxies.Clear();

    app.UseForwardedHeaders(forwardedHeadersOptions);

    /*app.UseHttpsRedirection();*/

    app.UseSerilogRequestLogging();

    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/api/health", new HealthCheckOptions());
    });

    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application Crashed: {ExceptionMessage}", e.Message); await Log.CloseAndFlushAsync(); throw;
}

public partial class Program { } // Helps reference the program in Unit tests
