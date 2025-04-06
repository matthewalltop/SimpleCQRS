
using Azure.Messaging.ServiceBus;
using Wolverine;
using Wolverine.AzureServiceBus;

public static class Program {

	public static async Task Main(string[] args) {
		var host = CreateHost(args);
		await host.RunAsync();

	}

	private static IHost CreateHost(string[] args)
		=> Host.CreateDefaultBuilder(args)
		       .ConfigureAppConfiguration(cfg => {
			       cfg.AddJsonFile("appsettings.json", false, true)
			          .AddEnvironmentVariables();
		       })
		       .ConfigureServices((hostCtx, services) => {
			       var serviceBusConn = hostCtx.Configuration.GetConnectionString("AzureServiceBus");
			       
			       services.AddWolverine(opts => {
				       
				       opts.Policies.DisableConventionalLocalRouting();
				       
				       opts.UseAzureServiceBus(serviceBusConn, config => {
					           config.RetryOptions.Mode = ServiceBusRetryMode.Exponential;
					           config.CustomEndpointAddress = new Uri("sb://localhost:5672");
				           })
				           .AutoProvision()
				           .UseConventionalRouting()
				           // .UseTopicAndSubscriptionConventionalRouting(cfg => {
					          //  cfg.TopicNameForListener(t => nameof(t));
					          //  cfg.SubscriptionNameForListener(t => $"{nameof(t)}-subscription");
				           // })
				           .AutoPurgeOnStartup()
				           .EnableWolverineControlQueues();


			       });



		       }).Build();

}