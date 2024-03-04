using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public record UpdateBillCommand(Guid BillId, UpdateBillInfo UpdateBillInfo) : ICommand<ErrorOr<Updated>>;