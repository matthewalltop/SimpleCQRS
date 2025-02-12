using Alba;
using Marten;

namespace SimpleCqrs.Command.Test;

[Collection("Integration Testing")]
public class ProductIntegrationTests: IntegrationContext {
    // TODO: Refactor to patterns.
    
    private readonly IAlbaHost _albaHost;
    public ProductIntegrationTests(AppFixture fixture) : base(fixture) {
        this._albaHost = fixture.Host;
    }

    [Theory, AutoData]
    public async Task AddsProductToStore(Guid productId, string name, string category, uint quantity)
    {
        var productAdded = new ProductAdded(productId, name, category, quantity);
        
        await using var session = Store.LightweightSession();

        session.Events.StartStream<Product>(productId, productAdded);
        
        await session.SaveChangesAsync();
        
        var result = await session.Events.FetchStreamAsync(productId);
        
        result.Should().NotBeNull().And.HaveCount(1);
    }
    
    [Theory, AutoData]
    public async Task AddsProductToStoreAndUpdatesQuantity(Guid productId, string name, string category, uint quantity)
    {
        var productAdded = new ProductAdded(productId, name, category, quantity);
        var productQtyUpdated = new ProductQuantityChanged(productId, 200);
        
        await using var session = Store.LightweightSession();

        session.Events.StartStream<Product>(productId, productAdded, productQtyUpdated);
        
        await session.SaveChangesAsync();
        
        var result = await session.Events.FetchStreamAsync(productId);
        
        result.Should().NotBeNull().And.HaveCount(2);
    }
    
    [Theory, AutoData]
    public async Task ProjectsEventsToProductAggregate(Guid productId, string name, string category, uint quantity)
    {
        var productAdded = new ProductAdded(productId, name, category, quantity);
        var productQtyUpdated = new ProductQuantityChanged(productId, 200);
        
        await using var session = Store.LightweightSession();

        session.Events.StartStream<Product>(productId, productAdded, productQtyUpdated);
        
        await session.SaveChangesAsync();

        var result = await session.Events.AggregateStreamAsync<Product>(productId);
        
        result.Should().NotBeNull();

        result.Id.Should().Be(productId);
        result.Name.Should().Be(name);
        result.Category.Should().Be(category);
        result.Quantity.Should().Be(200);
    }
}