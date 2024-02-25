using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public sealed record UpdatePaymentInfo(
	Guid? CreatorId,
	bool? IsCreatorIncluded,
	Amount? Amount,
	Guid[]? Debtors);