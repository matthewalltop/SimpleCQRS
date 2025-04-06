namespace MartenCqrs.Invoice.Entities;

public class Payment {
	
	public Guid Id { get; set; }
	public Guid? PayerId { get; set; }
	public decimal Amount { get; set; }
	public string PaymentMethod { get; set; }
	public DateTime PaymentDate { get; set; }

	public Payment(Guid id, Guid? payerId, decimal amount, string paymentMethod, DateTime paymentDate) {
		Id = id;
		PayerId = payerId;
		Amount = amount;
		PaymentMethod = paymentMethod;
		PaymentDate = paymentDate;
	}
}