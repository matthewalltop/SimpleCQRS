using Azure.Messaging.ServiceBus;
using Marten;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.AzureServiceBus;
using Wolverine.Marten;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication().UseWolverine(opts =>
{
    var postgresConn = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
    opts.Services.AddMarten(postgresConn).IntegrateWithWolverine();
    
    
    var azureServiceBusConnectionString = builder
        .Configuration
        .GetConnectionString("azure-service-bus");

    opts.UseAzureServiceBus(azureServiceBusConnectionString,
            azure => { azure.RetryOptions.Mode = ServiceBusRetryMode.Exponential; })
        .AutoProvision()
        .AutoPurgeOnStartup();
    
    opts.Policies.AutoApplyTransactions();

    opts.Durability.Mode = DurabilityMode.Serverless;
});

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();