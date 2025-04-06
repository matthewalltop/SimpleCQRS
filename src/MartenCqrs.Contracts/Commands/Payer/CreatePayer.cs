namespace MartenCqrs.Common.Commands.Payer;

using Wolverine;

public record CreatePayer(Guid CommandId, Guid PayerId, string PayerName): ICommand;