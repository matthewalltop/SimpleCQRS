namespace SimpleCqrs.Command.Application.UseCases;

using Marten;

// TODO: Abstract


public class ProductUseCaseHandler {

    private readonly IDocumentStore _documentStore;
    public ProductUseCaseHandler(IDocumentStore documentStore) {
        this._documentStore = documentStore;
    }
    
    public async Task Handle(AddProductRequest request) {
        await using var session = this._documentStore.LightweightSession();

        var productAdded = new ProductAdded(Guid.NewGuid(), 
            request.Name, 
            request.Category,
            request.Quantity);
        
        session.Events.Append(productAdded.Id, productAdded);
        
        await session.SaveChangesAsync();
    }

    public async Task Handle(AdjustProductQuantityRequest request) {
        await using var session = this._documentStore.LightweightSession();

        var productQtyAdjusted = new ProductQuantityChanged(request.Id, request.Quantity);

        session.Events.Append(productQtyAdjusted.Id, productQtyAdjusted);

        await session.SaveChangesAsync();
    }

    public async Task Handle(RemoveProductRequest request) {
        await using var session = this._documentStore.LightweightSession();

        var productRemoved = new ProductRemoved(request.Id);

        session.Events.Append(productRemoved.Id, productRemoved);

        await session.SaveChangesAsync();
    }

    public Task Handle<T>(T request) where T : CommandBase
    {
        throw new NotImplementedException();
    }
}