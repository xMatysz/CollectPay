using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public record UpdateBillCommand(Guid BillId, Guid UserId, UpdateBillInfo UpdateBillInfo) : ICommand<ErrorOr<Updated>>;

public record UpdateBillInfo(string Name, string[] EmailsToAdd, string[] EmailsToRemove);
