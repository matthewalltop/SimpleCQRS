namespace MartenCqrs.Common.Events.Payer;

using Wolverine;

public record PayerCreated: IEvent {
	public Guid Id { get; init; }
	public string Name { get; init; }

	public PayerCreated(Guid id, string name) {
		Id = id;
		Name = name;
	}
}