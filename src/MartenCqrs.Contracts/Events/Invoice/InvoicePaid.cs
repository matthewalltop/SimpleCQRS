namespace MartenCqrs.Common.Events.Invoice;

using Wolverine;

public record InvoicePaid: IEvent {
	public Guid InvoiceId { get; set; }
	public Guid PaymentId { get; set; }
	public Guid BillerId { get; set; }
	public decimal AmountPaid { get; set; }
	public string PaymentMethod { get; set; }
	public DateTime PaymentSubmissionDate { get; set; }
	
	public InvoicePaid(Guid invoiceId, Guid billerId, decimal amountPaid, 
	                   string paymentMethod, DateTime paymentSubmissionDate) {
		InvoiceId = invoiceId;
		BillerId = billerId;
		AmountPaid = amountPaid;
		PaymentMethod = paymentMethod;
		PaymentSubmissionDate = paymentSubmissionDate;
	}
}