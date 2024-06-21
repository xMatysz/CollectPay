using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;

public record RemovePaymentCommand(Guid UserId, Guid BillId, Guid PaymentId) : ICommand<ErrorOr<Deleted>>;