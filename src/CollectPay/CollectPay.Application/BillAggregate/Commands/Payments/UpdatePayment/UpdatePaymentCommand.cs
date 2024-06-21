using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public record UpdatePaymentCommand(
	Guid UserId,
	Guid BillId,
	Guid PaymentId,
	UpdatePaymentInfo UpdatePaymentInfo)
	: ICommand<ErrorOr<Updated>>;