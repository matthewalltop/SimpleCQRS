namespace SimpleCqrs.Command;

public class Product {
    public Product(ProductAdded product)
        => (Id, Name, Category, Quantity) = (product.Id, product.Name, product.Category, product.Quantity); 
    
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string? Category { get; private set; }
    public uint Quantity { get; private set; }
    public bool? Removed { get; private set; } = false;

    public void Apply(ProductQuantityChanged @event) 
        => Quantity = @event.NewQuantity;

    public void Apply(ProductRemoved @event)
        => Removed = true;
}

