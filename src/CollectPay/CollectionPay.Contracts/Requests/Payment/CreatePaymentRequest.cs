namespace CollectionPay.Contracts.Requests.Payment;

public record CreatePaymentRequest(
	Guid BillId,
	Guid CreatorId,
	bool IsCreatorIncluded,
	decimal Amount,
	string Currency,
	Guid[] Debtors);