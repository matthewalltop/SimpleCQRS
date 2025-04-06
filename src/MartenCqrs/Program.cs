using Biller.Domain;
using Marten;
using MartenCqrs.Common.Events.Invoice;
using MartenCqrs.Common.Events.Payer;
using Microsoft.AspNetCore.Hosting;
using Weasel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            typeof(BillerCreated),
            typeof(PayerCreated),
            typeof(InvoiceCreated),
            typeof(InvoicePaid)
        ]);

        return _;
    }).IntegrateWithWolverine();
});



var host = new HostBuilder()
    .ConfigureServices(configureServices)
    .ConfigureWebHostDefaults(b => b.Configure(_ => { }).ConfigureServices(configureServices))
    .Build();

host.Run();
