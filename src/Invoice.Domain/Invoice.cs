namespace Invoice.Domain;

using Enumerations;
using MartenCqrs.Common.Events.Invoice;
using MartenCqrs.Core;
using MartenCqrs.Invoice.Entities;

public class Invoice {

	public Guid Id { get; private set; }
	public Guid BillerId { get; private set; }
	public Guid PayerId { get; private set; }
	public DateTime GeneratedDate { get; private set; }
	public DateTime DueDate { get; private set; }
	public DateTime? PaidDate { get; private set; }
	public decimal Charges { get; private set; }
	public decimal Taxes { get; private set; }
	public decimal? Fees { get; private set; } = 0.0m;
	public decimal TotalCharges { get; private set; }
	
	public InvoiceStatus Status { get; private set; } = InvoiceStatus.Unpaid;

	private List<Payment> _payments = new();

	public IReadOnlyList<Payment> Payments => this._payments;
	

	public Invoice(InvoiceCreated @event) {
		Apply(@event);
	}

	public void Apply(InvoiceCreated @event) {
		Id = Guard.Against.NullOrEmptyGuid(nameof(Id), @event.InvoiceId);
		BillerId = Guard.Against.NullOrEmptyGuid(nameof(BillerId), @event.BillerId);
		PayerId = Guard.Against.NullOrEmptyGuid(nameof(PayerId), @event.PayerId);
		GeneratedDate = Guard.Against.NullOrEmptyDateTime(nameof(GeneratedDate), @event.GeneratedDate);
		DueDate = Guard.Against.NullOrEmptyDateTime(nameof(DueDate), @event.DueDate);
		Charges = Guard.Against.NegativeOrZero(nameof(Charges), @event.Charges);
		Taxes = Guard.Against.NegativeOrZero(nameof(Taxes), @event.Taxes);

		TotalCharges = Charges + Taxes + (Fees ?? 0.0m);
	}

	public void Apply(InvoicePaid @event) {
		var paymentId = Guard.Against.NullOrEmptyGuid(nameof(Id), @event.InvoiceId);
		var billerId = Guard.Against.NullOrEmptyGuid(nameof(BillerId), @event.BillerId);
		var paymentMethod = Guard.Against.NullOrEmptyString(nameof(Payment.PaymentMethod), @event.PaymentMethod);
		var paymentSubmissionDate = Guard.Against.NullOrEmptyDateTime(nameof(Payment.PaymentDate), @event.PaymentSubmissionDate);
		var amountPaid = Guard.Against.NegativeOrZero(nameof(Payment.Amount), @event.AmountPaid);
		var payment = new Payment(paymentId, billerId, amountPaid, paymentMethod, paymentSubmissionDate);
		
		this._payments.Add(payment);
		
		if (TotalCharges - @event.AmountPaid > 0.0m) {
			PaidDate = Guard.Against.NullOrEmptyDateTime(nameof(PaidDate), @event.PaymentSubmissionDate);
			Status = InvoiceStatus.Paid;
		} else {
			Status = InvoiceStatus.PartiallyPaid;
		}
	}

}