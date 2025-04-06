namespace Wolverine.Service.Handlers;

using Invoice.Domain;
using Marten;
using MartenCqrs.Common.Events.Invoice;

public class CreateInvoiceHandler {

	private readonly IDocumentStore _documentStore;
	private readonly ILogger<CreateInvoiceHandler> _logger;

	public CreateInvoiceHandler(IDocumentStore documentStore, ILogger<CreateInvoiceHandler> logger) {
		this._documentStore = documentStore;
		this._logger = logger;
	}
	
	public async Task ConsumeAsync(InvoiceCreated @event) {
		await using var session = this._documentStore.LightweightSession();

		session.Events.StartStream<Invoice>(@event.InvoiceId, @event);
		
		await session.SaveChangesAsync();
	}
}