namespace MartenCqrs.Common.Commands.Invoice;

using Wolverine;

public record CreateInvoice(Guid CommandId, Guid InvoiceId, 
                            Guid BillerId, Guid PayerId, 
                            DateTime InvoiceDate, DateTime DueDate, 
                            decimal Charges, decimal Taxes, decimal? Fees): ICommand;