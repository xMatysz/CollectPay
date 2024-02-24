using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bill.CreateBill;

public sealed record CreateBillCommand(
	Guid CreatorId,
	string BillName) : ICommand<Created>;