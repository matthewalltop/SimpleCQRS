namespace Wolverine.Service.Handlers;

using Invoice.Domain;
using Marten;
using Marten.Events;
using MartenCqrs.Common.Events.Invoice;

public class InvoicePaidHandler {

	private readonly IDocumentStore _documentStore;

	private readonly ILogger<InvoicePaidHandler> _logger;

	public InvoicePaidHandler(IDocumentStore documentStore, ILogger<InvoicePaidHandler> logger) {
		this._documentStore = documentStore;
		this._logger = logger;
	}
	
	public async Task ConsumeAsync(InvoicePaid @event) {
		await using var session = this._documentStore.LightweightSession();

		await session.Events.FetchStreamAsync(@event.InvoiceId);
		session.Events.Append(@event.InvoiceId, @event);
		
		await session.SaveChangesAsync();
	}
}