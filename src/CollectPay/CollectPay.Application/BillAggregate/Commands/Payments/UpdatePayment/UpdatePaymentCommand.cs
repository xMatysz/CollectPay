using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public record UpdatePaymentCommand(Guid BillId) : ICommand<ErrorOr<Updated>>;