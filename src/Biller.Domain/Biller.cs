namespace Biller.Domain;

using MartenCqrs.Core;

public class Biller {

    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public Biller(BillerCreated @event) {
        Apply(@event);
    }

    public void Apply(BillerCreated @event) {
        Id = Guard.Against.NullOrEmptyGuid(nameof(Id), @event.Id);
        Name = Guard.Against.NullOrEmptyString(nameof(Name), @event.Name);
    }
    
}