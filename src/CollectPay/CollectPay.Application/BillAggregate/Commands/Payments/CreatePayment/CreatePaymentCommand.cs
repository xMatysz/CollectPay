using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.ValueObjects;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;

public record CreatePaymentCommand(
	string Name,
	Guid BillId,
	Guid CreatorId,
	Amount Amount,
	Guid[] Debtors) : ICommand<ErrorOr<Created>>;