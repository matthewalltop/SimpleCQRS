namespace Biller.Domain;

using Wolverine;

public record BillerCreated: IEvent {
	public Guid Id { get; init; }
	public string Name { get; init; }

	public BillerCreated(Guid id, string name) {
		Id = id;
		Name = name;
	}
}