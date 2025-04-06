namespace SimpleCqrs.Command;

using System.Text.Json.Serialization;

public record ProductAdded {
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Category { get; init; }
    public uint Quantity { get; init; }
    
    [JsonConstructor]
    public ProductAdded() {}

    public ProductAdded(Guid id, string name, string category, uint quantity = 0)
        => (Id, Name, Category, Quantity) = (id, name, category, quantity);
}

public record ProductQuantityChanged {
    public Guid Id { get; init; }
    public uint NewQuantity { get; init; }

    [JsonConstructor]
    public ProductQuantityChanged() { }
    public ProductQuantityChanged(Guid id, uint newQuantity)
        => (Id, NewQuantity) = (id, newQuantity);
}

public record ProductRemoved {
    public Guid Id { get; init; }

    [JsonConstructor]
    public ProductRemoved() { }

    public ProductRemoved(Guid id)
        => Id = id;
}