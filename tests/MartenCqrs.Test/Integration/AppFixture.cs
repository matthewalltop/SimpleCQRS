using Alba;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using SimpleCqrs.Command.Bootstrap;
using Weasel.Core;

namespace SimpleCqrs.Command.Test;
public class AppFixture: IAsyncLifetime {
    private string SchemaName { get; } = "sch" + Guid.NewGuid().ToString().Replace("-", string.Empty);
    public IAlbaHost Host { get; private set; }

    public async Task InitializeAsync()
    {
        // This is bootstrapping the actual application using
        // its implied Program.Main() set up
        Host = await AlbaHost.For<Program>(b =>
        {
            b.ConfigureServices((_, services) =>
            {
                services.Configure<MartenSettings>(s => {
                    s.DatabaseSchemaName = SchemaName;
                });
                
                
                var postgresConn = "Host=localhost;Port=5432;Database=marten_test;Username=martendb;Password=martenpgsql";
    
                services.AddMarten(sp => {
        
                    var _ = new StoreOptions();
                    _.Connection(postgresConn);
                    _.UseSystemTextJsonForSerialization();
        
                    _.AutoCreateSchemaObjects = AutoCreate.All;
        
                    _.Events.AddEventTypes([
                        typeof(ProductAdded),
                        typeof(ProductQuantityChanged),
                        typeof(ProductRemoved)
                    ]);

                    return _;
                });
            });
        });
    }

    public async Task DisposeAsync() {
        await Host.DisposeAsync();
    }
}