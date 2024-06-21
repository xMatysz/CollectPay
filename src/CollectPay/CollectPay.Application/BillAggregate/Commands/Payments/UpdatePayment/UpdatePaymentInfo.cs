using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public sealed record UpdatePaymentInfo(
	string Name,
	Amount? Amount,
	Guid[] DebtorsToAdd,
	Guid[] DebtorsToRemove);