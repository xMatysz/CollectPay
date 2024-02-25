using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.ValueObjects;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;

public record CreatePaymentCommand(
	Guid BillId,
	Guid CreatorId,
	bool IsCreatorIncluded,
	Amount Amount,
	Guid[] Debtors) : ICommand<ErrorOr<Created>>;