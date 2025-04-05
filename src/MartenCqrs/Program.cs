using Marten;
using Microsoft.AspNetCore.Hosting;
using Weasel.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleCqrs.Command;
using Wolverine.Marten;

var configureServices = new Action<IServiceCollection>(services =>
{
    var postgresConn = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
    
    services.AddMarten(sp => {
        
        var _ = new StoreOptions();
        _.Connection(postgresConn);
        _.UseSystemTextJsonForSerialization();
        _.Events.MetadataConfig.EnableAll();
        _.Events.UseMandatoryStreamTypeDeclaration = true;
        
        _.AutoCreateSchemaObjects = AutoCreate.All;
        
        _.Events.AddEventTypes([
            typeof(ProductAdded),
            typeof(ProductQuantityChanged),
            typeof(ProductRemoved)
        ]);

        return _;
    }).IntegrateWithWolverine();
});



var host = new HostBuilder()
    .ConfigureServices(configureServices)
    .ConfigureWebHostDefaults(b => b.Configure(_ => { }).ConfigureServices(configureServices))
    .Build();

host.Run();
