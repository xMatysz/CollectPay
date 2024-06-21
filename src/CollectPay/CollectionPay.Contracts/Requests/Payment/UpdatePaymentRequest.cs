namespace CollectionPay.Contracts.Requests.Payment;

public record UpdatePaymentRequest(
	string Name,
	Guid BillId,
	Guid PaymentId,
	decimal? Amount,
	string? Currency,
	Guid[]? DebtorsToAdd,
	Guid[]? DebtorsToRemove);