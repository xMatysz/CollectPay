namespace CollectionPay.Contracts.Requests.Payment;

public record UpdatePaymentRequest(Guid BillId,
	Guid PaymentId,
	Guid? CreatorId,
	bool? IsCreatorIncluded,
	decimal? Amount,
	string? Currency,
	Guid[]? Debtors);