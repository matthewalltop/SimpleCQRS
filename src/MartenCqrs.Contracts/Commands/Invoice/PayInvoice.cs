namespace MartenCqrs.Common.Commands.Invoice;

using Wolverine;

public record PayInvoice: ICommand {

	public Guid CommandId { get; set; }
	public Guid InvoiceId { get; set; }
	
	public DateTime PaymentSubmissionDate { get; set; }
	public decimal AmountPaid { get; set; }
	public string PaymentMethod { get; set; }
}