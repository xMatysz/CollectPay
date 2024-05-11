using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;

public record RemoveBillCommand(Guid UserId, Guid BillId) : ICommand<ErrorOr<Deleted>>;