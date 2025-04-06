namespace MartenCqrs.Common.Events.Invoice;

using Wolverine;

public record InvoiceCreated: IEvent {
	public Guid InvoiceId { get; init; }
	public Guid BillerId { get; init; }
	public Guid PayerId { get; init; }
	public DateTime GeneratedDate { get; init; }
	public DateTime DueDate { get; init; }
	public decimal Charges { get; init; }
	public decimal Taxes { get; init; }
	public decimal Fees { get; init; } = 0.0m;
	
	public InvoiceCreated(Guid invoiceId, Guid billerId, Guid payerId, 
	                      DateTime generatedOn, DateTime dueDate, 
	                      decimal charges, decimal taxes, decimal fees) {
		InvoiceId = invoiceId;
		BillerId = billerId;
		PayerId = payerId;
		GeneratedDate = generatedOn;
		DueDate = dueDate;
		Charges = charges;
		Taxes = taxes;
		Fees = fees;
	}
}