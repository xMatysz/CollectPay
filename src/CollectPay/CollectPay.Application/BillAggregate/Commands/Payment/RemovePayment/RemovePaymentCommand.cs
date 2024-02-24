using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payment.RemovePayment;

public record RemovePaymentCommand(Guid BillId) : ICommand<Deleted>;