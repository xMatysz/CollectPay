using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payment.UpdatePayment;

public record UpdatePaymentCommand(Guid BillId) : ICommand<Updated>;