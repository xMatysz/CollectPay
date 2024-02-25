using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

public sealed record CreateBillCommand(
	Guid CreatorId,
	string BillName) : ICommand<ErrorOr<Created>>;