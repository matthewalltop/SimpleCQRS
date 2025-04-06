namespace Invoice.Domain.Enumerations;

using MartenCqrs.Core;

public partial record InvoiceStatus: Enumeration<InvoiceStatus> {
	public static readonly InvoiceStatus Unpaid = new(0, nameof(Unpaid));
	public static readonly InvoiceStatus PartiallyPaid = new(1, nameof(PartiallyPaid));
	public static readonly InvoiceStatus Paid = new(2, nameof(Paid));
	
	public InvoiceStatus() {}
	public InvoiceStatus(int value, string displayName) : base(value, displayName) { }
};