namespace CollectionPay.Contracts.Requests;

public record GetDebtsResponse(Guid Debtor, decimal DebtAmount, Guid Creditor);