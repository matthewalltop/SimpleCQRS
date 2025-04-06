namespace MartenCqrs.Common.Commands.Biller;

using Wolverine;

public record CreateBiller(Guid CommandId, Guid BillerId, string BillerName);