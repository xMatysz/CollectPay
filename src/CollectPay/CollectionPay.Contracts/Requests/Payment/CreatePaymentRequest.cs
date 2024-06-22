namespace CollectionPay.Contracts.Requests.Payment;

public record CreatePaymentRequest(
	string Name,
	Guid BillId,
	decimal Amount,
	string Currency,
	Guid[] Debtors);