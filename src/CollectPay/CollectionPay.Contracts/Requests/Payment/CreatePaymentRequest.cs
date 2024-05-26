namespace CollectionPay.Contracts.Requests.Payment;

public record CreatePaymentRequest(
	string Name,
	Guid BillId,
	bool IsCreatorIncluded,
	decimal Amount,
	string Currency,
	Guid[] Debtors);