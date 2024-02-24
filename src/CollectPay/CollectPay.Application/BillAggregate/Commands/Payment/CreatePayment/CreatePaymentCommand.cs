using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payment.CreatePayment;

public record CreatePaymentCommand(Guid BillId) : ICommand<Created>;