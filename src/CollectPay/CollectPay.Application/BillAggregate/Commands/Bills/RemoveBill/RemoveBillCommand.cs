using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;

public record RemoveBillCommand(Guid BillId) : ICommand<ErrorOr<Deleted>>;